// <copyright file="Library.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module LambdaInterpreter

/// <summary>
/// Represents an untyped lambda calculus term.
/// </summary>
type Term =
    /// <summary>Variable with a given name.</summary>
    | Variable of string
    /// <summary>Abstraction (lambda) binding a variable in a body.</summary>
    | Abstraction of string * Term
    /// <summary>Application of a function to an argument.</summary>
    | Application of Term * Term

/// <summary>
/// Returns the set of free variables in a term.
/// </summary>
/// <param name="term">The lambda term.</param>
/// <returns>Set of variable names that occur free in the term.</returns>
let rec freeVars term =
    match term with
    | Variable x -> Set.singleton x
    | Abstraction (x, body) -> Set.remove x (freeVars body)
    | Application (l, r) -> Set.union (freeVars l) (freeVars r)

/// <summary>
/// Renames all free occurrences of <paramref name="oldName"/> to <paramref name="newName"/> in a term.
/// Does not rename variables bound by an inner binder with the same name (avoids shadowing).
/// </summary>
/// <param name="oldName">The variable name to rename.</param>
/// <param name="newName">The new variable name.</param>
/// <param name="term">The term to rename within.</param>
/// <returns>A new term with renamed free variables.</returns>
let rec renameFreeVar oldName newName term =
    match term with
    | Variable x when x = oldName -> Variable newName
    | Variable x -> Variable x
    | Application (l, r) ->
        Application (renameFreeVar oldName newName l, renameFreeVar oldName newName r)
    | Abstraction (x, body) when x = oldName -> Abstraction (x, body)
    | Abstraction (x, body) ->
        Abstraction (x, renameFreeVar oldName newName body)

/// <summary>
/// Generates a fresh variable name not present in <paramref name="usedVars"/>,
/// based on <paramref name="baseName"/>.
/// </summary>
/// <param name="usedVars">Set of variable names already in use.</param>
/// <param name="baseName">Base name to start from.</param>
/// <returns>A variable name not in <paramref name="usedVars"/>.</returns>
let rec freshVar usedVars baseName =
    if Set.contains baseName usedVars then
        freshVar usedVars (baseName + "'")
    else
        baseName

/// <summary>
/// Substitutes term <paramref name="n"/> for variable <paramref name="x"/> in <paramref name="term"/>.
/// Performs alpha-conversion to avoid variable capture when necessary.
/// </summary>
/// <param name="x">The variable to substitute for.</param>
/// <param name="n">The term to substitute in place of <paramref name="x"/>.</param>
/// <param name="term">The term in which to perform substitution.</param>
/// <returns>A new term with the substitution applied.</returns>
let rec substitute x n term =
    match term with
    | Variable y when y = x -> n
    | Variable y -> Variable y
    | Application (l, r) ->
        Application (substitute x n l, substitute x n r)
    | Abstraction (y, body) when y = x ->
        Abstraction (y, body)
    | Abstraction (y, body) ->
        let fvN = freeVars n
        if Set.contains y fvN && Set.contains x (freeVars body) then
            let used = Set.unionMany [freeVars n; freeVars body; Set.singleton x]
            let z = freshVar used y
            let body' = renameFreeVar y z body
            Abstraction (z, substitute x n body')
        else
            Abstraction (y, substitute x n body)

/// <summary>
/// Performs a single beta-reduction step on the leftmost outermost redex.
/// Returns <c>None</c> if the term is already in normal form.
/// </summary>
/// <param name="term">The term to reduce.</param>
/// <returns><c>Some</c> reduced term, or <c>None</c> if no redex exists.</returns>
let rec reduceOnce term =
    match term with
    | Application (Abstraction (x, body), arg) ->
        Some (substitute x arg body)
    | Application (l, r) ->
        match reduceOnce l with
        | Some l' -> Some (Application (l', r))
        | None ->
            match reduceOnce r with
            | Some r' -> Some (Application (l, r'))
            | None -> None
    | Abstraction (x, body) ->
        match reduceOnce body with
        | Some body' -> Some (Abstraction (x, body'))
        | None -> None
    | Variable _ -> None

/// <summary>
/// Reduces a lambda term to its normal form using normal-order (leftmost outermost) reduction.
/// </summary>
/// <param name="term">The term to reduce.</param>
/// <returns>The normal form of the term.</returns>
let rec normalize term =
    match reduceOnce term with
    | Some term' -> normalize term'
    | None -> term

/// <summary>
/// Converts a lambda term to its string representation.
/// </summary>
/// <param name="term">The term to convert.</param>
/// <returns>A human-readable string representation of the term.</returns>
let rec toString term =
    match term with
    | Variable x -> x
    | Abstraction (x, body) -> $"\\%s{x}.%s{toString body}"
    | Application (l, r) ->
        let ls =
            match l with
            | Abstraction _ -> $"(%s{toString l})"
            | _ -> toString l
        let rs =
            match r with
            | Abstraction _ | Application _ -> $"(%s{toString r})"
            | _ -> toString r

        $"%s{ls} %s{rs}"

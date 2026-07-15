// <copyright file="Library.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module ParsingTree

/// Represents a binary arithmetic operation.
type Operation =
    | Add
    | Sub
    | Mul
    | Div

/// Represents an arithmetic expression tree.
type ExprTree =
    | Const of int
    | Operator of Operation * ExprTree * ExprTree

/// Evaluates an arithmetic expression tree using CPS.
/// <param name="expr">The expression tree to evaluate.</param>
/// <param name="cont">The continuation to call with the result.</param>
/// <returns>The result of the continuation.</returns>
let rec calculate expr cont =
    match expr with
    | Const x -> cont (Ok x)
    | Operator (op, left, right) ->
        calculate left (fun resL ->
            calculate right (fun resR ->
                match resL, resR with
                | Ok l, Ok r ->
                    match op with
                    | Div when r = 0 -> cont (Error "Division by zero")
                    | Add -> cont (Ok (l + r))
                    | Sub -> cont (Ok (l - r))
                    | Mul -> cont (Ok (l * r))
                    | Div -> cont (Ok (l / r))
                | Error e, _ | _, Error e -> cont (Error e)
            )
        )

/// Evaluates an expression tree and returns the result.
/// <param name="expr">The expression tree to evaluate.</param>
/// <returns>Ok with the integer result, or Error with a description.</returns>
let calculateExpr expr = calculate expr id 

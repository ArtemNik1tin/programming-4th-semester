// <copyright file="Program.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module Task2

/// <summary>
/// Prints a square made of asterisks with side length n.
/// </summary>
/// <param name="n">Side length of the square.</param>
/// <returns>
/// List of strings representing square rows. Empty list for n <= 0.
/// </returns>
let printSquare n =
    match n with
    | n when n <= 0 -> []
    | 1 -> [ "*" ]
    | 2 -> [ "**"; "**" ]
    | n ->
        let border = String.replicate n "*"
        let middle = "*" + String.replicate (n - 2) " " + "*"
        let middleLines = List.replicate (n - 2) middle
        border :: middleLines @ [ border ]

[<EntryPoint>]
let main args =
    match args with
    | [| nStr |] ->
        match System.Int32.TryParse nStr with
        | true, n ->
            printSquare n |> String.concat "\n" |> printfn "%s"
            0
        | false, _ ->
            printfn "Error: invalid number"
            1
    | _ ->
        printfn "Usage: Task2 <n>"
        0

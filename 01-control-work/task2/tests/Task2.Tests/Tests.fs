// <copyright file="Tests.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module Tests

open NUnit.Framework
open FsUnit

[<Test>]
let ``printSquare returns empty list for n = 0`` () = Task2.printSquare 0 |> should be Empty

[<Test>]
let ``printSquare returns empty list for negative n`` () = Task2.printSquare -5 |> should be Empty

[<Test>]
let ``printSquare returns single star for n = 1`` () =
    Task2.printSquare 1 |> should equal [ "*" ]

[<Test>]
let ``printSquare returns full square for n = 2`` () =
    Task2.printSquare 2 |> should equal [ "**"; "**" ]

[<Test>]
let ``printSquare returns hollow square for n = 3`` () =
    Task2.printSquare 3 |> should equal [ "***"; "* *"; "***" ]

[<Test>]
let ``printSquare returns hollow square for n = 4`` () =
    Task2.printSquare 4 |> should equal [ "****"; "*  *"; "*  *"; "****" ]

[<Test>]
let ``printSquare returns correct size for n = 5`` () =
    let result = Task2.printSquare 5
    result.Length |> should equal 5
    result.Head |> should equal "*****"
    List.last result |> should equal "*****"

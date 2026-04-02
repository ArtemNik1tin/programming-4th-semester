// <copyright file="Task1Tests.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module Task1.Tests

open NUnit.Framework
open FsUnit

[<Test>]
let ``findMin returns None for empty list`` () =
    ListUtils.findMin [] |> should equal None

[<Test>]
let ``findMin returns the only element for single-element list`` () =
    ListUtils.findMin [ 5 ] |> should equal (Some 5)

[<Test>]
let ``findMin returns smallest element for positive numbers`` () =
    ListUtils.findMin [ 3; 1; 4; 1; 5; 9; 2; 6 ] |> should equal (Some 1)

[<Test>]
let ``findMin returns smallest element for negative numbers`` () =
    ListUtils.findMin [ -5; -3; -7; -1 ] |> should equal (Some -7)

[<Test>]
let ``findMin returns smallest element for mixed numbers`` () =
    ListUtils.findMin [ -2; 5; -8; 3; 0 ] |> should equal (Some -8)

// <copyright file="Tests.fs" company="Task3">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module Task3.Tests

open NUnit.Framework
open FsUnit

[<Test>]
let ``Add stores key-value pair`` () =
    let ht = Task3.HashTable<string, int>(hash, 10)
    ht.Add("key", 42)
    ht.Contains("key") |> should equal true

[<Test>]
let ``Contains returns true for existing key`` () =
    let ht = Task3.HashTable<string, int>(hash, 10)
    ht.Add("a", 1)
    ht.Contains("a") |> should equal true

[<Test>]
let ``Remove deletes key and returns true`` () =
    let ht = Task3.HashTable<string, int>(hash, 10)
    ht.Add("b", 2)
    ht.Remove("b") |> should equal true
    ht.Contains("b") |> should equal false

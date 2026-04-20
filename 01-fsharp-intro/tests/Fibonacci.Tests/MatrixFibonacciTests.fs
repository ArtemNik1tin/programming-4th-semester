// <copyright file="MatrixFibonacciTests.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module FibonacciTests

open NUnit.Framework
open FsUnit

[<TestCase(-1L, 1L)>]
[<TestCase(-2L, -1L)>]
[<TestCase(-3L, 2L)>]
[<TestCase(-10L, -55L)>]
[<TestCase(0L, 0L)>]
[<TestCase(1L, 1L)>]
[<TestCase(2L, 1L)>]
[<TestCase(10L, 55L)>]
let ``getFibonacciNumber should return correct num for base cases`` (n, expected) =
    MatrixFibonacciUtils.getFibonacciNumber n |> should equal expected

[<Test>]
let ``F92 is the largest Fibonacci fitting in uint64`` () =
    MatrixFibonacciUtils.getFibonacciNumber 92 |> should equal 7540113804746346429L

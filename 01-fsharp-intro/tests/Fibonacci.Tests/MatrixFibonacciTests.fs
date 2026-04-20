// <copyright file="MatrixFibonacciTests.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module FibonacciTests

open NUnit.Framework
open FsUnit

[<TestCase(0UL, 0UL)>]
[<TestCase(1UL, 1UL)>]
[<TestCase(2UL, 1UL)>]
[<TestCase(10UL, 55UL)>]
let ``getFibonacci should return correct num for base cases`` (n, expected) =
    MatrixFibonacciUtils.getFibonacciNumber n |> should equal expected

[<Test>]
let ``F92 is the largest Fibonacci fitting in uint64`` () =
    MatrixFibonacciUtils.getFibonacciNumber 92UL
    |> should equal 7540113804746346429UL

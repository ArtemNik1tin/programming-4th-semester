module FactorialTests

open NUnit.Framework
open FsUnit
open Factorial

[<Test>]
let ``Factorial of 0 should be 1`` () =
    calculateFactorial 0UL |> should equal 1UL

[<TestCase(1UL, 1UL)>]
[<TestCase(5UL, 120UL)>]
[<TestCase(10UL, 3628800UL)>]
[<TestCase(20UL, 2432902008176640000UL)>]
let ``Check known factorial values`` (input, expected) =
    calculateFactorial input |> should equal expected

namespace FibonacciCore.Tests

module FibonacciTests =
    open NUnit.Framework
    open FsUnit
    open FibonacciCore

    [<Test>]
    let ``getFibonacci should return 0 for 0`` () =
        MatrixFib.getFibonacci 0UL |> should equal 0UL

    [<Test>]
    let ``getFibonacci should return 1 for 1`` () =
        MatrixFib.getFibonacci 1UL |> should equal 1UL

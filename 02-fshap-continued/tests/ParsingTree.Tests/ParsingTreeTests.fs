namespace ParsingTree.Tests

open NUnit.Framework
open FsUnit
open ParsingTree

module ParsingTreeTests =
    let ok x: Result<int, string> = Ok x
    let err x: Result<int, string> = Error x

    [<Test>]
    let ``calculate should evaluate a single constant`` () =
        Const 5
        |> calculateExpr
        |> should equal (ok 5)

    [<Test>]
    let ``calculate should evaluate simple addition`` () =
        Operator(Add, Const 2, Const 3)
        |> calculateExpr
        |> should equal (ok 5)

    [<Test>]
    let ``calculate should evaluate subtraction`` () =
        Operator(Sub, Const 10, Const 3)
        |> calculateExpr
        |> should equal (ok 7)

    [<Test>]
    let ``calculate should evaluate multiplication`` () =
        Operator(Mul, Const 6, Const 7)
        |> calculateExpr
        |> should equal (ok 42)

    [<Test>]
    let ``calculate should evaluate division`` () =
        Operator(Div, Const 10, Const 2)
        |> calculateExpr
        |> should equal (ok 5)

    [<Test>]
    let ``calculate should respect operator precedence via tree structure`` () =
        Operator(Add,
            Const 5,
            Operator(Mul, Const 3, Const 4))
        |> calculateExpr
        |> should equal (ok 17)

    [<Test>]
    let ``calculate should handle complex nested expression`` () =
        Operator(Sub,
            Operator(Mul,
                Operator(Add, Const 1, Const 2),
                Const 3),
            Const 4)
        |> calculateExpr
        |> should equal (ok 5)

    [<Test>]
    let ``calculate should return error on division by zero`` () =
        Operator(Div, Const 5, Const 0)
        |> calculateExpr
        |> should equal (err "Division by zero")

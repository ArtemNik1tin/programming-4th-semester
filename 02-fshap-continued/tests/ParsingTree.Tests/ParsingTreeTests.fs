namespace ParsingTree.Tests

open NUnit.Framework
open FsUnit
open TreeADT
open ParsingTree

module ParsingTreeTests =
    [<Test>]
    let ``calculateLinearize should evaluate a single constant`` () =
        Node(Const 5, Empty, Empty)
        |> calculateLinearize
        |> should equal 5

    [<Test>]
    let ``calculateLinearize should evaluate simple addition`` () =
        Node(Add,
            Node(Const 2, Empty, Empty),
            Node(Const 3, Empty, Empty))
        |> calculateLinearize
        |> should equal 5

    [<Test>]
    let ``calculateLinearize should evaluate subtraction`` () =
        Node(Sub,
            Node(Const 10, Empty, Empty),
            Node(Const 3, Empty, Empty))
        |> calculateLinearize
        |> should equal 7

    [<Test>]
    let ``calculateLinearize should evaluate multiplication`` () =
        Node(Mul,
            Node(Const 6, Empty, Empty),
            Node(Const 7, Empty, Empty))
        |> calculateLinearize
        |> should equal 42

    [<Test>]
    let ``calculateLinearize should evaluate division`` () =
        Node(Div,
            Node(Const 10, Empty, Empty),
            Node(Const 2, Empty, Empty))
        |> calculateLinearize
        |> should equal 5

    [<Test>]
    let ``calculateLinearize should respect operator precedence via tree structure`` () =
        Node(Add,
            Node(Const 5, Empty, Empty),
            Node(Mul,
                Node(Const 3, Empty, Empty),
                Node(Const 4, Empty, Empty)))
        |> calculateLinearize
        |> should equal 17

    [<Test>]
    let ``calculateLinearize should handle complex nested expression`` () =
        Node(Sub,
            Node(Mul,
                Node(Add,
                    Node(Const 1, Empty, Empty),
                    Node(Const 2, Empty, Empty)),
                Node(Const 3, Empty, Empty)),
            Node(Const 4, Empty, Empty))
        |> calculateLinearize
        |> should equal 5

    [<Test>]
    let ``calculateLinearize should throw on division by zero`` () =
        (fun () ->
            Node(Div,
                Node(Const 5, Empty, Empty),
                Node(Const 0, Empty, Empty))
            |> calculateLinearize
            |> ignore)
        |> should throw typeof<System.Exception>

    [<Test>]
    let ``calculateLinearize should throw on empty tree`` () =
        (fun () -> calculateLinearize Empty |> ignore)
        |> should throw typeof<System.Exception>

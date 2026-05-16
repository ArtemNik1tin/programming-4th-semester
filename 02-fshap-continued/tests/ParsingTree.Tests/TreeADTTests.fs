namespace ParsingTree.Tests

open NUnit.Framework
open FsUnit
open TreeADT

module TreeADTTests =
    let rec collect acc = function
        | Finished -> List.rev acc
        | Step(x, next) -> collect (x :: acc) (next())

    [<Test>]
    let ``linearizePostOrder should flatten single node`` () =
        let tree = Node(1, Empty, Empty)
        let steps = linearizePostOrder tree (fun () -> Finished)

        collect [] steps |> should equal [ 1 ]

    [<Test>]
    let ``linearizePostOrder should flatten left-leaning tree`` () =
        let tree = Node(3, Node(2, Node(1, Empty, Empty), Empty), Empty)
        let steps = linearizePostOrder tree (fun () -> Finished)

        collect [] steps |> should equal [ 1; 2; 3 ]

    [<Test>]
    let ``linearizePostOrder should flatten right-leaning tree`` () =
        let tree = Node(1, Empty, Node(2, Empty, Node(3, Empty, Empty)))
        let steps = linearizePostOrder tree (fun () -> Finished)

        collect [] steps |> should equal [ 3; 2; 1 ]

    [<Test>]
    let ``linearizePostOrder should flatten full tree in post-order`` () =
        let tree = Node(2, Node(1, Empty, Empty), Node(3, Empty, Empty))
        let steps = linearizePostOrder tree (fun () -> Finished)

        collect [] steps |> should equal [ 1; 3; 2 ]

    [<Test>]
    let ``linearizePostOrder should flatten complex tree`` () =
        let tree = Node(4,
                    Node(2,
                        Node(1, Empty, Empty),
                        Node(3, Empty, Empty)),
                    Node(6,
                        Node(5, Empty, Empty),
                        Node(7, Empty, Empty)))
        let steps = linearizePostOrder tree (fun () -> Finished)

        collect [] steps |> should equal [ 1; 3; 2; 5; 7; 6; 4 ]

    [<Test>]
    let ``iter should traverse in post-order`` () =
        let tree = Node(2, Node(1, Empty, Empty), Node(3, Empty, Empty))
        let result = ResizeArray()

        iter (fun x cont next -> result.Add(x); cont next) tree

        result |> should equal (ResizeArray [ 1; 3; 2 ])

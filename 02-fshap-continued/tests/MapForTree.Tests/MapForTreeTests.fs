namespace MapForTree.Tests

open NUnit.Framework
open FsUnit
open MapForTree

module MapForTreeTests =
    [<Test>]
    let ``map should return Empty for Empty tree`` () =
        mapTree id Empty |> should equal Empty

    [<Test>]
    let ``map should transform single node`` () =
        let tree = Node(1, Empty, Empty)
        mapTree (fun x -> x * 2) tree |> should equal (Node(2, Empty, Empty))

    [<Test>]
    let ``map should transform full binary tree`` () =
        let tree = Node(2, Node(1, Empty, Empty), Node(3, Empty, Empty))
        mapTree (fun x -> x * 10) tree
        |> should equal (Node(20, Node(10, Empty, Empty), Node(30, Empty, Empty)))

    [<Test>]
    let ``map should preserve structure of complex tree`` () =
        let tree = Node(4,
                    Node(2,
                        Node(1, Empty, Empty),
                        Node(3, Empty, Empty)),
                    Node(6,
                        Node(5, Empty, Empty),
                        Node(7, Empty, Empty)))
        let result = mapTree (fun x -> x * x) tree
        let expected = Node(16,
                        Node(4,
                            Node(1, Empty, Empty),
                            Node(9, Empty, Empty)),
                        Node(36,
                            Node(25, Empty, Empty),
                            Node(49, Empty, Empty)))
        result |> should equal expected

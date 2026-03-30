namespace ReverseList.Tests

open NUnit.Framework
open FsUnit
open ReverseList

module ReverseListTests =

    [<Test>]
    let ``getReverseList should return empty list for empty input list`` () =
        ReverseList.getReverseList [] |> should equal []

    [<Test>]
    let ``getReverseList should return reverse list for simple cases`` () =
        ReverseList.getReverseList [ 1; 2; 3 ] |> should equal [ 3; 2; 1 ]
        ReverseList.getReverseList [ 1; -2 ] |> should equal [ -2; 1 ]
        ReverseList.getReverseList [ -1; 2 ] |> should equal [ 2; -1 ]

    [<Test>]
    let ``getReverseList should return the same list for lists with same values`` () =
        ReverseList.getReverseList [ 0 ] |> should equal [ 0 ]
        ReverseList.getReverseList [ 0; 0; 0; 0; 0 ] |> should equal [ 0; 0; 0; 0; 0 ]
        ReverseList.getReverseList [ -1; -1 ] |> should equal [ -1; -1 ]

namespace NumberSearch.Tests

module NumberSearchTests =
    open FsUnit
    open NUnit.Framework
    open NumberSearch

    [<TestCase(1, 0)>]
    [<TestCase(2, 1)>]
    [<TestCase(3, 2)>]
    [<TestCase(4, 3)>]
    [<TestCase(5, 4)>]
    let ``getFirstOccurrence should return correct index for positive numbers`` (inputNumber, expectedIndex) =
        let result = NumberSearch.getFirstOccurrence [ 1; 2; 3; 4; 5 ] inputNumber

        match result with
        | Some(resultIndex) -> resultIndex |> should equal expectedIndex
        | None -> failwith "result is None"

    [<TestCase(-200, 1)>]
    let ``getFirstOccurrence should return correct index for negative numbers`` (inputNumber, expectedIndex) =
        let result = NumberSearch.getFirstOccurrence [ 1; -200; 3; 4; 5 ] inputNumber

        match result with
        | Some(resultIndex) -> resultIndex |> should equal expectedIndex
        | None -> failwith "result is None"

    [<Test>]
    let ``getFirstOccurrence should return correct index for empty list`` () =
        NumberSearch.getFirstOccurrence [] -10 |> should equal None

module RoundingUpWorkflow.Tests

open NUnit.Framework
open FsUnit
open RoundingUpWorkflow

[<Test>]
let ``example from task`` () =
    let result = rounding 3 {
        let! a = 2.0 / 12.0
        let! b = 3.5
        return a / b
    }
    result |> should equal 0.048

[<Test>]
let ``precision 0 rounds to integer`` () =
    let result = rounding 0 {
        let! a = 1.7
        return a
    }
    result |> should equal 2.0

[<Test>]
let ``precision 0 rounds down`` () =
    let result = rounding 0 {
        let! a = 1.3
        return a
    }
    result |> should equal 1.0

[<Test>]
let ``precision 4 gives more accurate result`` () =
    let result = rounding 4 {
        let! a = 1.0 / 3.0
        return a
    }
    result |> should equal 0.3333

[<Test>]
let ``multiple operations with intermediate rounding`` () =
    let result = rounding 2 {
        let! a = 1.234
        let! b = 2.345
        return a + b
    }
    result |> should equal 3.58

[<Test>]
let ``division after rounding`` () =
    let result = rounding 2 {
        let! a = 5.0
        let! b = 3.0
        return a / b
    }
    result |> should equal 1.67

[<Test>]
let ``chained operations`` () =
    let result = rounding 1 {
        let! a = 1.44
        let! b = 2.0
        let! c = 3.0
        return a * b + c
    }
    result |> should equal 5.8

[<Test>]
let ``negative numbers work correctly`` () =
    let result = rounding 2 {
        let! a = -1.234
        return a
    }
    result |> should equal -1.23

[<Test>]
let ``return! rounds correctly`` () =
    let result = rounding 2 {
        return! 3.456
    }
    result |> should equal 3.46

[<Test>]
let ``return! with let! rounds both`` () =
    let result = rounding 2 {
        let! x = 1.234
        return! x + 2.0
    }
    result |> should equal 3.23

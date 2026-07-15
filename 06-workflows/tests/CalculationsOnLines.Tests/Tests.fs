module CalculationsOnLines.Tests

open NUnit.Framework
open FsUnit
open CalculationsOnLines

[<Test>]
let ``valid strings sum correctly`` () =
    let result = calculate {
        let! x = "1"
        let! y = "2"
        return x + y
    }
    result |> should equal (Some 3)

[<Test>]
let ``invalid string returns None`` () =
    let result = calculate {
        let! x = "1"
        let! y = "Ъ"
        return x + y
    }
    result |> should equal None

[<Test>]
let ``first invalid string short-circuits`` () =
    let result = calculate {
        let! x = "abc"
        let! y = "2"
        return x + y
    }
    result |> should equal None

[<Test>]
let ``zero parses correctly`` () =
    let result = calculate {
        let! x = "0"
        return x
    }
    result |> should equal (Some 0)

[<Test>]
let ``negative numbers parse correctly`` () =
    let result = calculate {
        let! x = "-5"
        let! y = "3"
        return x + y
    }
    result |> should equal (Some -2)

[<Test>]
let ``empty string returns None`` () =
    let result = calculate {
        let! x = ""
        return x
    }
    result |> should equal None

[<Test>]
let ``non-numeric string returns None`` () =
    let result = calculate {
        let! x = "hello"
        return x
    }
    result |> should equal None

[<Test>]
let ``multiple valid operations`` () =
    let result = calculate {
        let! a = "10"
        let! b = "20"
        let! c = "30"
        return a + b + c
    }
    result |> should equal (Some 60)

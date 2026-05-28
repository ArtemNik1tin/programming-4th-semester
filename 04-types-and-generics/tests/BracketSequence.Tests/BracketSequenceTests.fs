module BracketSequenceTests

open NUnit.Framework
open FsUnit
open BracketSequence

[<Test>]
let ``Empty string should be valid`` () =
    CheckBracketBalance "" |> should be True

[<Test>]
let ``String without brackets should be valid`` () =
    CheckBracketBalance "text without brackets 123" |> should be True

[<Test>]
let ``Simple balanced parentheses should be valid`` () =
    CheckBracketBalance "(())" |> should be True

[<Test>]
let ``Different types of brackets in a row should be valid`` () =
    CheckBracketBalance "()[]{}" |> should be True

[<Test>]
let ``Complex nested brackets should be valid`` () =
    CheckBracketBalance "([{}])" |> should be True

[<Test>]
let ``Brackets with text inside should be valid`` () =
    CheckBracketBalance "abv(a[0], {value})" |> should be True

[<Test>]
let ``Extra closing bracket should be invalid`` () =
    CheckBracketBalance "())" |> should be False

[<Test>]
let ``Unclosed opening bracket should be invalid`` () =
    CheckBracketBalance "(()" |> should be False

[<Test>]
let ``Interleaved brackets should be invalid`` () =
    CheckBracketBalance "([)]" |> should be False

[<Test>]
let ``Mismatched bracket types should be invalid`` () =
    CheckBracketBalance "(}" |> should be False

[<Test>]
let ``Single closing bracket should be invalid`` () =
    CheckBracketBalance "]" |> should be False

[<Test>]
let ``Correct brackets in reverse order should be invalid`` () =
    CheckBracketBalance ")(" |> should be False

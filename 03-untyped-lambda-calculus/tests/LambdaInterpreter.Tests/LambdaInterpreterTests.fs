// <copyright file="LambdaInterpreterTests.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module LambdaInterpreter.Tests

open NUnit.Framework
open FsUnit
open LambdaInterpreter

[<Test>]
let ``identity reduces correctly`` () =
    normalize (Application (Abstraction ("x", Variable "x"), Variable "a"))
    |> should equal (Variable "a")

[<Test>]
let ``constant function reduces correctly`` () =
    normalize (
        Application (
            Application (Abstraction ("x", Abstraction ("y", Variable "x")), Variable "a"),
            Variable "b"
        )
    )
    |> should equal (Variable "a")

[<Test>]
let ``alpha conversion avoids variable capture`` () =
    normalize (Application (Abstraction ("x", Abstraction ("y", Variable "x")), Variable "y"))
    |> should equal (Abstraction ("y'", Variable "y"))

[<Test>]
let ``self application reduces to identity`` () =
    normalize (
        Application (
            Abstraction ("x", Application (Variable "x", Variable "x")),
            Abstraction ("y", Variable "y")
        )
    )
    |> should equal (Abstraction ("y", Variable "y"))

[<Test>]
let ``normal order reduces outermost redex first`` () =
    normalize (
        Application (
            Abstraction ("x", Abstraction ("y", Variable "y")),
            Application (Abstraction ("z", Variable "z"), Variable "a")
        )
    )
    |> should equal (Abstraction ("y", Variable "y"))

[<Test>]
let ``variable is in normal form`` () =
    normalize (Variable "x") |> should equal (Variable "x")

[<Test>]
let ``abstraction is in normal form`` () =
    normalize (Abstraction ("x", Variable "x"))
    |> should equal (Abstraction ("x", Variable "x"))

[<Test>]
let ``application with variable function is in normal form`` () =
    normalize (Application (Variable "x", Variable "y"))
    |> should equal (Application (Variable "x", Variable "y"))

[<Test>]
let ``nested abstraction reduces body`` () =
    normalize (
        Abstraction ("x", Application (Abstraction ("y", Variable "y"), Variable "x"))
    )
    |> should equal (Abstraction ("x", Variable "x"))

[<Test>]
let ``church numeral 2 applied to identity`` () =
    normalize (
        Application (
            Abstraction ("f",
                Abstraction ("x",
                    Application (Variable "f",
                        Application (Variable "f", Variable "x")))),
            Abstraction ("y", Variable "y")
        )
    )
    |> should equal (Abstraction ("x", Variable "x"))

[<Test>]
let ``toString prints variable`` () =
    toString (Variable "x") |> should equal "x"

[<Test>]
let ``toString prints abstraction`` () =
    toString (Abstraction ("x", Variable "x")) |> should equal "\\x.x"

[<Test>]
let ``toString prints application`` () =
    toString (Application (Variable "x", Variable "y")) |> should equal "x y"

[<Test>]
let ``toString prints application with abstraction on left`` () =
    toString (Application (Abstraction ("x", Variable "x"), Variable "a"))
    |> should equal "(\\x.x) a"

[<Test>]
let ``toString prints application with abstraction on right`` () =
    toString (Application (Variable "a", Abstraction ("x", Variable "x")))
    |> should equal "a (\\x.x)"

[<Test>]
let ``toString prints nested application`` () =
    toString (Application (Application (Variable "x", Variable "y"), Variable "z"))
    |> should equal "x y z"

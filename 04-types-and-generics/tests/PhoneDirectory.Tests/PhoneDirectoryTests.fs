module PhoneDirectoryTests

open NUnit.Framework
open FsUnit
open PhoneDirectory

[<Test>]
let ``add should increase directory size`` () =
    let initial = []
    let updated = add "Ivan" "123" initial
    updated.Length |> should equal 1
    updated.[0].Name |> should equal "Ivan"

[<Test>]
let ``findPhones should return multiple numbers for same name`` () =
    let db = [] |> add "Ivan" "111" |> add "Ivan" "222"
    let results = findPhones "Ivan" db
    results |> should contain "111"
    results |> should contain "222"
    results.Length |> should equal 2

[<Test>]
let ``findNames should return multiple names for same phone`` () =
    let db = [] |> add "Ivan" "111" |> add "Oleg" "111"
    let results = findNames "111" db
    results |> should contain "Ivan"
    results |> should contain "Oleg"
    results.Length |> should equal 2

[<Test>]
let ``deserialize should return error on corrupted lines`` () =
    let input = [ "Ivan;123"; "corrupted line"; ";"; "Petr;456" ]

    match deserialize input with
    | Error _ -> Assert.Pass()
    | Ok _ -> Assert.Fail("Expected error for corrupted lines")

[<Test>]
let ``serialize and deserialize should be consistent`` () =
    let initial =
        [ { PhoneDirectory.Name = "Test"
            PhoneDirectory.Phone = "999" } ]

    match initial |> serialize |> deserialize with
    | Ok result -> result |> should equal initial
    | Error _ -> Assert.Fail("Expected success")
namespace PhoneDirectory.Tests

module PhoneDirectoryTests =
    open NUnit.Framework
    open FsUnit
    open PhoneDirectory

    [<Test>]
    let ``add should increase directory size`` () =
        let initial = []
        let updated = PhoneDirectory.add "Ivan" "123" initial
        updated.Length |> should equal 1
        updated.[0].Name |> should equal "Ivan"

    [<Test>]
    let ``findPhones should return multiple numbers for same name`` () =
        let db = [] |> PhoneDirectory.add "Ivan" "111" |> PhoneDirectory.add "Ivan" "222"
        let results = PhoneDirectory.findPhones "Ivan" db
        results |> should contain "111"
        results |> should contain "222"
        results.Length |> should equal 2

    [<Test>]
    let ``findNames should return multiple names for same phone`` () =
        let db = [] |> PhoneDirectory.add "Ivan" "111" |> PhoneDirectory.add "Oleg" "111"
        let results = PhoneDirectory.findNames "111" db
        results |> should contain "Ivan"
        results |> should contain "Oleg"
        results.Length |> should equal 2

    [<Test>]
    let ``deserialize should skip corrupted lines`` () =
        let input = [ "Ivan;123"; "corrupted line"; ";"; "Petr;456" ]
        let db = PhoneDirectory.deserialize input
        db.Length |> should equal 2
        let names = db |> List.map (fun e -> e.Name)
        names |> should contain "Ivan"
        names |> should contain "Petr"

    [<Test>]
    let ``serialize and deserialize should be consistent`` () =
        let initial =
            [ { PhoneDirectory.Name = "Test"
                PhoneDirectory.Phone = "999" } ]

        let result = initial |> PhoneDirectory.serialize |> PhoneDirectory.deserialize
        result |> should equal initial

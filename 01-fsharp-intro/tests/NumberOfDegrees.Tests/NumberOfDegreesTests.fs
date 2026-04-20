module NumberOfDegreesTests

open NUnit.Framework
open FsUnit
open NumberOfDegrees

[<Test>]
let ``getListOfPowersOfTwoFromNToM should return None for negative numbers`` () =
    getListOfPowersOfTwoFromNToM 3 -1 |> should equal None
    getListOfPowersOfTwoFromNToM -3 -1 |> should equal None

[<Test>]
let ``getListOfPowersOfTwoFromNToM should return correct Lists for simple cases`` () =
    match getListOfPowersOfTwoFromNToM 1 2 with
    | Some result -> result |> should equal [ 2.0; 4.0; 8.0 ]
    | None -> failwith "getListOfPowersOfTwoFromNToM has returned None"

    match getListOfPowersOfTwoFromNToM 0 0 with
    | Some result -> result |> should equal [ 1.0 ]
    | None -> failwith "getListOfPowersOfTwoFromNToM has returned None"

    match getListOfPowersOfTwoFromNToM 0 100 with
    | Some result -> result |> should equal [ for i in 1..101 -> 2.0 ** float (i - 1) ]
    | None -> failwith "getListOfPowersOfTwoFromNToM has returned None"

    match getListOfPowersOfTwoFromNToM 5 1 with
    | Some result -> result |> should equal [ 32.0; 64.0 ]
    | None -> failwith "getListOfPowersOfTwoFromNToM has returned None"

[<Test>]
let ``getListOfPowersOfTwoFromNToM should return correct Lists for negative cases`` () =
    let epsilon = 0.000001

    match getListOfPowersOfTwoFromNToM -1 2 with
    | Some result -> abs 0.25 - result.Item 2 < epsilon |> should equal true
    | None -> failwith "getListOfPowersOfTwoFromNToM has returned None"

    match getListOfPowersOfTwoFromNToM 5 1 with
    | Some result -> abs 0.03125 - result.Item 0 < epsilon |> should equal true
    | None -> failwith "getListOfPowersOfTwoFromNToM has returned None"

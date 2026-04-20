module NumberOfDegreesTests

open NUnit.Framework
open FsUnit
open NumberOfDegrees

[<Test>]
let ``getListOfPowersOfTwoFromNToM should return None for incorrect list size`` () =
    getListOfPowersOfTwoFromNToM 5 1 |> should equal None

[<Test>]
let ``getListOfPowersOfTwoFromNToM should return None for negative numbers`` () =
    getListOfPowersOfTwoFromNToM -3 1 |> should equal None
    getListOfPowersOfTwoFromNToM -3 -1 |> should equal None

let ``getListOfPowersOfTwoFromNToM should return correct Lists for simple cases`` () =
    match getListOfPowersOfTwoFromNToM 1 2 with
    | Some result -> result |> should equal [ 2; 4 ]
    | None -> failwith "getListOfPowersOfTwoFromNToM has returned None"

    match getListOfPowersOfTwoFromNToM 0 0 with
    | Some result -> result |> should equal []
    | None -> failwith "getListOfPowersOfTwoFromNToM has returned None"

    match getListOfPowersOfTwoFromNToM 1 10 with
    | Some result -> result |> should equal [ 2; 4; 8; 16; 32; 64; 128; 256; 512; 1024 ]
    | None -> failwith "getListOfPowersOfTwoFromNToM has returned None"

// <copyright file="Tests.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module LocalNetwork.Tests

open NUnit.Framework
open FsUnit
open LocalNetwork

/// <summary>
/// Mock random generator that returns values from a predefined list cyclically.
/// </summary>
type MockRandomGenerator(values: float list) =
    let mutable index = 0
    interface IRandomGenerator with
        member _.NextDouble() =
            let v = values[index % values.Length]
            index <- index + 1
            v

let private createNetwork matrix computers rng =
    match Network.Create(matrix, computers, rng) with
    | Ok net -> net
    | Error msg -> failwith $"Unexpected error: {msg}"

[<Test>]
let ``infection spreads like BFS when probability is 1`` () =
    let mockRng = MockRandomGenerator([ 0.0 ])
    let os = OperatingSystem("TestOS", 1.0)
    let computers =
        [ Computer(os, true)
          Computer(os, false)
          Computer(os, false)
          Computer(os, false) ]
    let matrix = array2D [
        [ false; true;  false; false ]
        [ true;  false; true;  false ]
        [ false; true;  false; true  ]
        [ false; false; true;  false ]
    ]
    let network = createNetwork matrix computers mockRng

    network.Step() |> should equal true
    computers[0].IsInfected |> should equal true
    computers[1].IsInfected |> should equal true
    computers[2].IsInfected |> should equal false
    computers[3].IsInfected |> should equal false

    network.Step() |> should equal true
    computers[2].IsInfected |> should equal true
    computers[3].IsInfected |> should equal false

    network.Step() |> should equal true
    computers[3].IsInfected |> should equal true

    network.Step() |> should equal false

[<Test>]
let ``no infection when probability is 0`` () =
    let mockRng = MockRandomGenerator([ 0.5 ])
    let os = OperatingSystem("TestOS", 0.0)
    let computers =
        [ Computer(os, true)
          Computer(os, false) ]
    let matrix = array2D [ [ false; true ]; [ true; false ] ]
    let network = createNetwork matrix computers mockRng
    network.Step() |> should equal false
    computers[1].IsInfected |> should equal false

[<Test>]
let ``infection cannot jump over intermediate node in one step`` () =
    let mockRng = MockRandomGenerator([ 0.0 ])
    let os = OperatingSystem("TestOS", 1.0)
    let computers =
        [ Computer(os, true)
          Computer(os, false)
          Computer(os, false) ]
    let matrix = array2D [
        [ false; true;  false ]
        [ true;  false; true  ]
        [ false; true;  false ]
    ]
    let network = createNetwork matrix computers mockRng

    network.Step() |> should equal true
    computers[1].IsInfected |> should equal true
    computers[2].IsInfected |> should equal false

    network.Step() |> should equal true
    computers[2].IsInfected |> should equal true

[<Test>]
let ``no infection without infected neighbours`` () =
    let mockRng = MockRandomGenerator([ 0.0 ])
    let os = OperatingSystem("TestOS", 1.0)
    let computers =
        [ Computer(os, false)
          Computer(os, false)
          Computer(os, false) ]
    let matrix = array2D [
        [ false; true; false ]
        [ true;  false; true  ]
        [ false; true;  false ]
    ]
    let network = createNetwork matrix computers mockRng
    network.Step() |> should equal false

[<Test>]
let ``fully disconnected network never infects`` () =
    let mockRng = MockRandomGenerator([ 0.0 ])
    let os = OperatingSystem("TestOS", 1.0)
    let computers =
        [ Computer(os, true)
          Computer(os, false)
          Computer(os, false) ]
    let matrix = array2D [
        [ false; false; false ]
        [ false; false; false ]
        [ false; false; false ]
    ]
    let network = createNetwork matrix computers mockRng
    network.Step() |> should equal false
    computers[1].IsInfected |> should equal false
    computers[2].IsInfected |> should equal false

[<Test>]
let ``Create returns Error for mismatched matrix size`` () =
    let rng = MockRandomGenerator([ 0.0 ])
    let os = OperatingSystem("TestOS", 1.0)
    let computers = [ Computer(os); Computer(os) ]
    let matrix = array2D [ [ false; true; false ]; [ true; false; true ]; [ false; true; false ] ]

    match Network.Create(matrix, computers, rng) with
    | Error msg -> msg |> should equal "Adjacency matrix size must equal the number of computers"
    | Ok _ -> failwith "Expected Error but got Ok"

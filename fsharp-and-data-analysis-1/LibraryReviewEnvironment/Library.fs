module Library

open FSharp.Data

[<Literal>]
let ResolutionFolder = __SOURCE_DIRECTORY__

type Stocks = CsvProvider<"./data/MSFT.csv", ResolutionFolder=ResolutionFolder>

[<EntryPoint>]
let main argv =
    let msft = Stocks.Load(__SOURCE_DIRECTORY__ + "/data/MSFT.csv").Cache()

    let firstRow = msft.Rows |> Seq.head
    let lastDate = firstRow.Date
    let lastOpen = firstRow.Open

    printfn $"Last date: {lastDate}, Open: {lastOpen}"

    // Print the first 10 prices in the HLOC format
    for row in msft.Rows |> Seq.truncate 10 do
        printfn "HLOC: (%A, %A, %A, %A)" row.High row.Low row.Open row.Close

    0
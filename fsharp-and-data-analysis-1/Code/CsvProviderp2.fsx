// Получаем первую строку
let firstRow = msft.Rows |> Seq.head
let lastDate = firstRow.Date
let lastOpen = firstRow.Open

printfn $"Дата: {lastDate}, Открытие: {lastOpen}"

// Выводим первые 10 строк в формате HLOC
printfn "\nПервые 10 записей (High, Low, Open, Close):"
for row in msft.Rows |> Seq.truncate 10 do
    printfn "HLOC: (%A, %A, %A, %A)"
        row.High
        row.Low
        row.Open
        row.Close
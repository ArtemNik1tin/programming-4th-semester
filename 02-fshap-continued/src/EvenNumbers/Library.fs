module EvenNumbers

let calculateEvenNumbersUsingMap source =
    List.sum (List.map (fun x -> 1 - abs x % 2) source)

let calculateEvenNumbersUsingFilter source =
    (List.filter (fun x -> x % 2 = 0) source).Length

let calculateEvenNumbersUsingFold source =
    List.fold (fun acc x -> acc + (1 - abs x % 2)) 0 source

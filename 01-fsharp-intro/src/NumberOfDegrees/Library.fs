module NumberOfDegrees

let getListOfPowersOfTwoFromNToM n m =
    match m with
    | _ when m < 0 -> None
    | _ ->
        let list = List.replicate (m - n + 1) 1
        Some(List.mapi (fun i x -> x <<< i + n) list)

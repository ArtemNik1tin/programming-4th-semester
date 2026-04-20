module NumberOfDegrees

let getListOfPowersOfTwoFromNToM n m =
    match m with
    | _ when m < 0 -> None
    | _ ->
        let initList = [ for _ in 1 .. int (m + 1) -> 2 ]

        let rec fillInList acc list k =
            match list with
            | [] -> acc
            | head :: tail -> fillInList (float head ** (float n + float k) :: acc) tail (k - 1)

        Some(fillInList [] initList m)

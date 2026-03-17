namespace NumberOfDegrees

module NumberOfDegrees =
    let getListOfPowersOfTwoFromNToM (n: int) (m: int) : Option<list<int>> =
        if m < n - 1 then
            None
        elif m < 0 || n < 0 then
            None
        else
            let list = List.replicate (m - n + 1) 1
            Some(List.mapi (fun i x -> x <<< i + n) list)

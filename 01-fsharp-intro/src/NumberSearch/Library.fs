namespace NumberSearch

module NumberSearch =
    let getFirstOccurrence searchSpace desired =
        let rec loop searchSpace desired index =
            match searchSpace with
            | head :: _ when head = desired -> Some(index)
            | _ :: tail -> loop tail desired (index + 1)
            | [] -> None

        loop searchSpace desired 0

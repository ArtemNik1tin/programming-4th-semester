namespace NumberSearch

module NumberSearch =
    let rec getFirstOccurrence searchSpace desired =
        let rec loop searchSpace desired index =
            match searchSpace with
            | head :: tail ->
                if head = desired then
                    Some(index)
                else
                    loop tail desired (index + 1)
            | [] -> None

        loop searchSpace desired 0

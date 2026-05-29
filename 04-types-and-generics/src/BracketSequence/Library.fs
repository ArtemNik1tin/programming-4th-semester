module BracketSequence

let CheckBracketBalance input =
    let openingBracket = set [ '('; '['; '{' ]
    let isOpening c = openingBracket.Contains(c)
    let closingBracket = set [ ')'; ']'; '}' ]
    let isClosing c = closingBracket.Contains(c)

    let isMatching stackChar currentChar =
        match stackChar, currentChar with
        | '(', ')' -> true
        | '[', ']' -> true
        | '{', '}' -> true
        | _ -> false

    let rec loop stack remainingChars =
        match stack, remainingChars with
        | [], [] -> true
        | _, [] -> false
        | currentStack, currentChar :: tail when isOpening currentChar ->
            loop (currentChar :: currentStack) tail
        | currentStack, currentChar :: tail when isClosing currentChar ->
            match currentStack with
            | top :: restOfStack when isMatching top currentChar -> loop restOfStack tail
            | _ -> false
        | currentStack, _ :: tail ->
            loop currentStack tail

    let chars = input |> Seq.toList
    loop [] chars

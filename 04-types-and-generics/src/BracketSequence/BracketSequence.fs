module internal BracketSequenceImpl

let CheckBracketBalance input =
    let openingBracket = set [ '('; '['; '{' ]
    let isOpening c = openingBracket.Contains(c)
    let closingBracket = set [ ')'; ']'; '}' ]
    let isClosing c = closingBracket.Contains(c)

    let isMatching stackChar currentChar =
        if not (isOpening stackChar && isClosing currentChar) then
            false
        elif stackChar = '(' && currentChar = ')' then
            true
        elif stackChar = '[' && currentChar = ']' then
            true
        elif stackChar = '{' && currentChar = '}' then
            true
        else
            false

    let rec loop stack remaningChars =
        match stack, remaningChars with
        | [], [] -> true
        | _, [] -> false
        | currentStack, currentChar :: tail ->
            if isOpening currentChar then
                loop (currentChar :: currentStack) tail
            elif isClosing currentChar then
                match currentStack with
                | top :: restOfStack when isMatching top currentChar -> loop restOfStack tail
                | _ -> false
            else
                loop currentStack tail

    let chars = input |> Seq.toList
    loop [] chars

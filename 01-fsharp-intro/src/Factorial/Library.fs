module Factorial

let calculateFactorial n =
    let rec calculateProductInInterval left right =
        match left, right with
        | _ when left > right -> 1UL
        | _ when left = right -> left
        | _ when right - left = 1UL -> left * right
        | _ ->
            let m = (left + right) / 2UL
            calculateProductInInterval left m * calculateProductInInterval (m + 1UL) right

    let factorialTree n =
        match n with
        | 0UL -> 1UL
        | 1UL
        | 2UL -> n
        | _ -> calculateProductInInterval 2UL n

    factorialTree n

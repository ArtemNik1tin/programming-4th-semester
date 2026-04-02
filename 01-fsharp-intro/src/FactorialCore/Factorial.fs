module FactorialImpl

let rec calculateProductInInterval left right =
    if left > right then
        1
    elif left = right then
        left
    elif right - left = 1 then
        left * right
    else
        let m = (left + right) / 2

        calculateProductInInterval left m * calculateProductInInterval (m + 1) right

let factorialTree n =
    match n with
    | 0 -> 1
    | 1
    | 2 -> n
    | _ -> calculateProductInInterval 2 n

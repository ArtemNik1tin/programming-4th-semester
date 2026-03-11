module FactorialImpl

let rec calculateProductInInterval (left: uint64) (right: uint64) : uint64 =
    if left > right then
        uint64 1
    elif left = right then
        left
    elif right - left = uint64 1 then
        left * right
    else
        let m = (left + right) / uint64 2

        calculateProductInInterval left m
        * calculateProductInInterval (m + uint64 1) right

let factorialTree (n: uint64) : uint64 =
    if n = uint64 0 then uint64 1
    elif n = uint64 1 || n = uint64 2 then n
    else calculateProductInInterval (uint64 2) n

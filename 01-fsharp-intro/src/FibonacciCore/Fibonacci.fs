module internal FibonacciImpl

open System.Numerics

let rec raiseXToPowerN (x: seq<seq<uint64>>)  (n: uint64) idMatrix matrixMultiply =
    match n with
    | 0UL -> idMatrix
    | 1UL -> x
    | _ ->
        let y = raiseXToPowerN x (n / 2UL) idMatrix matrixMultiply
        let ySquared = matrixMultiply y y
        if n % 2UL = 1UL then matrixMultiply x ySquared
        else ySquared
 
let multiplyMatrices (a: seq<seq<uint64>>) (b: seq<seq<uint64>>): seq<seq<uint64>> =
    let bTranspose = Seq.transpose b
    Seq.map (fun row ->
        Seq.map (fun col ->
            Seq.zip row col |> Seq.sumBy (fun (n, m) -> n * m)) bTranspose) a

let getIdMatrix (size: int): seq<seq<uint64>> =
    Seq.init size (fun rowIndex -> Seq.init size (fun colIndex -> if rowIndex = colIndex then 1UL else 0UL))

let getFibonacciNumber (n: uint64): uint64 =
    if n = 0UL then 0UL
    else if n = 1UL then 1UL
    else
        let F = raiseXToPowerN (List.toSeq [List.toSeq [1UL; 1UL]; List.toSeq [1UL; 0UL]]) n (getIdMatrix 2) multiplyMatrices
        Seq.item 1 (Seq.item 0 F)

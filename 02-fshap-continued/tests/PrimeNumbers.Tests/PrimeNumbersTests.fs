module PrimeNumbersTests

open NUnit.Framework
open FsUnit
open PrimeNumbers

let littleSeqSize = 3
let largeSeqSize = 10

[<Test>]
let ``getPrimeNumbersSeq returns prime numbers sequence`` () =
    let primeList = List.ofSeq (Seq.take 10 getPrimeNumbersSeq)

    let expected = [ 2; 3; 5; 7; 11; 13; 17; 19; 23; 29 ]


    primeList |> should equal expected

[<Test>]
let ``getPrimeNumbersSeq returns at least one number`` () =
    let primeSeq = getPrimeNumbersSeq

    Seq.takeWhile (fun elem -> elem < littleSeqSize) primeSeq
    |> should not' (be Null)

[<Test>]
let ``getPrimeNumbersSeq contains first prime number`` () =
    let primeSeq = getPrimeNumbersSeq

    Seq.takeWhile (fun elem -> elem < littleSeqSize) primeSeq |> should contain 2

[<Test>]
let ``getPrimeNumbersSeq shouldn't return 1`` () =
    let primeSeq = getPrimeNumbersSeq

    Seq.takeWhile (fun elem -> elem < littleSeqSize) primeSeq
    |> should not' (contain 1)

[<Test>]
let ``getPrimeNumbersSeq should have type seq<int>`` () =
    let primeSeq = getPrimeNumbersSeq

    Seq.takeWhile (fun elem -> elem < littleSeqSize) primeSeq
    |> should be instanceOfType<seq<int>>

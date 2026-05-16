// <copyright file="Library.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module PrimeNumbers

/// <summary>
/// Generates an infinite sequence of prime numbers.
/// </summary>
let getPrimeNumbersSeq =
    let isPrime n =
        seq {2 .. (float n |> sqrt |> int)} |> Seq.forall (fun d -> n % d <> 0)

    let infinitePrimeNumbers n =
        Seq.initInfinite ((+) n) |> Seq.filter isPrime

    infinitePrimeNumbers 2

// <copyright file="Library.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module PrimeNumbers

/// <summary>
/// Generates an infinite sequence of prime numbers.
/// </summary>
let getPrimeNumbersSeq =
    let isPrime n =
        let rec next isPrime divider =
            match isPrime, divider with
            | false, _ -> false
            | true, 1 -> true
            | true, divider when divider = n -> next true (divider - 1)
            | true, divider -> next (not (n % divider = 0)) (divider - 1)

        next true (int (ceil (float n ** (1.0 / 2.0))))

    let rec infinitePrimeNumbers n =
        seq {
            match n with
            | n when isPrime n -> yield! Seq.append (Seq.singleton n) (infinitePrimeNumbers (n + 1))
            | _ -> yield! infinitePrimeNumbers (n + 1)
        }

    infinitePrimeNumbers 2

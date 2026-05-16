// Copyright (c) 2026. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the repository root for full license information.

module NumberOfDegrees

/// <summary>
/// Returns a list of powers of two from 2^n to 2^(n+m) inclusive.
/// </summary>
/// <param name="n">The starting exponent.</param>
/// <param name="m">The number of additional powers to generate. Must be non-negative.</param>
/// <returns>Some list of length m+1, or None if m is negative.</returns>
let getListOfPowersOfTwoFromNToM n m =
    match m with
    | _ when m < 0 -> None
    | _ ->
        let initValue = float 2 ** float n

        let rec fillInList acc k =
            match k with
            | 0 -> List.rev acc
            | _ ->
                match acc with
                | acc_head :: _ -> fillInList ((acc_head * float 2) :: acc) (k - 1)
                | [] -> fillInList acc k

        Some(fillInList [ initValue ] m)

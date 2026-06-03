// <copyright file="Library.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module CalculationsOnLines

/// <summary>
/// Computation expression builder for calculations on string-represented numbers.
/// Each <c>let!</c> parses a string to <c>int</c>; if any parse fails the whole
/// expression yields <c>None</c>.
/// </summary>
type CalculateBuilder() =

    /// <summary>
    /// Parses <paramref name="value"/> as an integer and passes it to the
    /// continuation <paramref name="f"/>. Returns <c>None</c> on parse failure.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="f">The continuation receiving the parsed integer.</param>
    /// <returns><c>Some</c> result of <paramref name="f"/> or <c>None</c>.</returns>
    member _.Bind(value: string, f: int -> Option<int>) =
        match System.Int32.TryParse(value) with
        | true, n -> f n
        | false, _ -> None

    /// <summary>
    /// Wraps a value into <c>Some</c>.
    /// </summary>
    /// <param name="value">The integer value.</param>
    /// <returns><c>Some value</c>.</returns>
    member _.Return(value: int) = Some value

    /// <summary>
    /// Passes through an existing <c>Option&lt;int&gt;</c> value.
    /// </summary>
    /// <param name="value">An optional integer.</param>
    /// <returns><paramref name="value"/> unchanged.</returns>
    member _.ReturnFrom(value: Option<int>) = value

/// <summary>
/// Workflow for safe integer arithmetic on string inputs.
/// </summary>
let calculate = CalculateBuilder()

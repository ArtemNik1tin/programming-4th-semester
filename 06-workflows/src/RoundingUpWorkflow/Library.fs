// <copyright file="Library.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module RoundingUpWorkflow

open System

/// <summary>
/// Computation expression builder that rounds intermediate and final
/// floating-point results to a given number of decimal places.
/// </summary>
/// <param name="precision">Number of decimal places (0 &lt;= precision &lt;= 15).</param>
type RoundingBuilder(precision: int) =

    /// <summary>
    /// Rounds <paramref name="value"/> to <c>precision</c> decimal places,
    /// then passes it to the continuation <paramref name="f"/>.
    /// </summary>
    /// <param name="value">The floating-point value to round.</param>
    /// <param name="f">The continuation receiving the rounded value.</param>
    /// <returns>Result of <paramref name="f"/>.</returns>
    member _.Bind(value: float, f: float -> float) =
        f (Math.Round(value, precision))

    /// <summary>
    /// Rounds the final result to <c>precision</c> decimal places.
    /// </summary>
    /// <param name="value">The value to return.</param>
    /// <returns><paramref name="value"/> rounded to <c>precision</c> places.</returns>
    member _.Return(value: float) =
        Math.Round(value, precision)

    /// <summary>
    /// Rounds an already-computed result.
    /// </summary>
    /// <param name="value">The value to return.</param>
    /// <returns><paramref name="value"/> rounded to <c>precision</c> places.</returns>
    member _.ReturnFrom(value: float) =
        Math.Round(value, precision)

/// <summary>
/// Creates a <see cref="RoundingBuilder" /> with the given precision.
/// </summary>
/// <param name="precision">Number of decimal places.</param>
/// <returns>A <c>RoundingBuilder</c> instance.</returns>
let rounding precision = RoundingBuilder(precision)

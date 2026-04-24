// <copyright file="Library.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Task1

module ListUtils =
    /// <summary>Finds the smallest element in a list.</summary>
    /// <param name="list">Input list of comparable elements.</param>
    /// <returns>Some smallest element, or None if the list is empty.</returns>
    let findMin list =
        match list with
        | [] -> None
        | head :: tail -> Some(List.fold (fun acc x -> if x < acc then x else acc) head tail)

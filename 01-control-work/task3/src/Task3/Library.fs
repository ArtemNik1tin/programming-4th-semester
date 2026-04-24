// <copyright file="Library.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Task3

/// <summary>
/// A hash table with external hash function.
/// </summary>
/// <param name="hashFunc">Hash function for keys.</param>
/// <param name="initialCapacity">Initial number of buckets.</param>
type HashTable<'Key, 'Value when 'Key: equality>(hashFunc: 'Key -> int, initialCapacity: int) =
    let buckets: ('Key * 'Value) list array = Array.create initialCapacity []

    let getIndex key = abs (hashFunc key) % buckets.Length

    let existsInBucket bucket key =
        List.exists (fun (k, _) -> k = key) bucket

    /// <summary>
    /// Adds a key-value pair to the hash table.
    /// </summary>
    /// <param name="key">The key to add.</param>
    /// <param name="value">The value to associate with the key.</param>
    member ht.Add(key, value) =
        let index = getIndex key
        let bucket = buckets.[index]

        if not (existsInBucket bucket key) then
            buckets.[index] <- (key, value) :: bucket

    /// <summary>
    /// Checks if the hash table contains the specified key.
    /// </summary>
    /// <param name="key">The key to search for.</param>
    /// <returns>True if the key exists, false otherwise.</returns>
    member ht.Contains(key) =
        let index = getIndex key
        existsInBucket buckets.[index] key

    /// <summary>
    /// Removes the key-value pair associated with the specified key.
    /// </summary>
    /// <param name="key">The key to remove.</param>
    /// <returns>True if the key was found and removed, false otherwise.</returns>
    member ht.Remove(key) =
        let index = getIndex key
        let bucket = buckets.[index]

        match List.tryFindIndex (fun (k, _) -> k = key) bucket with
        | Some _ ->
            buckets.[index] <- List.filter (fun (k, _) -> k <> key) bucket
            true
        | None -> false

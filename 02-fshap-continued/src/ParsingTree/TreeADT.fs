// <copyright file="TreeADT.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module TreeADT

/// A generic binary tree data structure.
type Tree<'a> =
    /// A tree node with a value and left/right subtrees.
    | Node of 'a * Tree<'a> * Tree<'a>
    /// An empty tree (leaf).
    | Empty

/// Represents a step in a linearized tree traversal.
type ContinuationStep<'a> =
    /// Signals the end of traversal.
    | Finished
    /// A step containing a value and a continuation that yields the next step.
    | Step of 'a * (unit -> ContinuationStep<'a>)

/// Linearizes a binary tree into a sequence of steps using post-order traversal
/// (left subtree, right subtree, root).
/// <param name="binTree">The binary tree to linearize.</param>
/// <param name="cont">The continuation to call when traversal is complete.</param>
/// <returns>The first step in the linearized sequence.</returns>
let rec linearizePostOrder binTree cont =
    match binTree with
    | Empty -> cont()
    | Node(x, l, r) ->
        linearizePostOrder l (fun () -> linearizePostOrder r (fun () -> Step(x, cont)))

/// Iterates over a binary tree using the linearized post-order traversal,
/// applying a function to each node's value.
/// <param name="f">The function to apply to each node value.</param>
/// <param name="binTree">The binary tree to iterate over.</param>
let iter f binTree =
    let steps = linearizePostOrder binTree (fun () -> Finished)
    
    let rec processSteps step =
        match step with
            | Finished -> ()
            | Step(x, getNext) -> f x processSteps (getNext())
    processSteps steps

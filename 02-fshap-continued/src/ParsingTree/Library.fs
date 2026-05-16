// <copyright file="Library.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module ParsingTree

open TreeADT

/// Represents an arithmetic expression node.
type Expr =
    /// A constant integer value.
    | Const of int
    /// Addition operator.
    | Add
    /// Subtraction operator.
    | Sub
    /// Multiplication operator.
    | Mul
    /// Division operator.
    | Div
    
/// Represents an arithmetic expression tree built from Expr nodes.
type ExprTree = Tree<Expr>

/// Evaluates an arithmetic expression tree using a post-order linearized traversal
/// with a stack-based approach.
/// <param name="expr">The expression tree to evaluate.</param>
/// <returns>The integer result of evaluating the expression.</returns>
/// <exception cref="System.Exception">Thrown when division by zero occurs or the expression is invalid.</exception>
let calculateLinearize expr =
    let rec loop stack = function
        | Finished ->
            match stack with
            | [res] -> res
            | _ -> failwith "Invalid expression"
        | Step(Const n, next) -> loop (n :: stack) (next())
        | Step(Add, next) ->
            match stack with
            | b :: a :: rest -> loop (a + b :: rest) (next())
            | _ -> failwith "Invalid expression"
        | Step(Sub, next) ->
            match stack with
            | b :: a :: rest -> loop (a - b :: rest) (next())
            | _ -> failwith "Invalid expression"
        | Step(Mul, next) ->
            match stack with
            | b :: a :: rest -> loop (a * b :: rest) (next())
            | _ -> failwith "Invalid expression"
        | Step(Div, next) ->
            match stack with
            | b :: a :: rest ->
                match b with
                | 0 -> failwith "Division by zero"
                | _ -> loop (a / b :: rest) (next())
            | _ -> failwith "Invalid expression"

    loop [] (linearizePostOrder expr (fun () -> Finished)) 

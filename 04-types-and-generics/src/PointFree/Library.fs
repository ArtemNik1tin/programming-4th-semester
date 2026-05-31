module PointFree

/// Multiply each element of a list by a given factor.
///
/// Step-by-step transformation to point-free style:
let multiplyEachOriginal x l = List.map (fun y -> y * x) l
let multiplyEachCommutativity x l = List.map (fun y -> x * y) l
let multiplyEachOperatorAsFunction x l = List.map ((*) x) l
let multiplyEachReduction x = List.map ((*) x)
let multiplyEachComposition = (*) >> List.map
let multiplyEach = (*) >> List.map

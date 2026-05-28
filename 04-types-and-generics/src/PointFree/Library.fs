module PointFree

/// Multiply each element of a list by a given factor.
///
/// Step-by-step transformation to point-free style:
///
///   multiplyEach x l = List.map (fun y -> y * x) l    -- Original
///   multiplyEach x l = List.map ((*) x) l              -- Operator as function: (fun y -> y * x) = ((*) x)
///   multiplyEach x   = List.map ((*) x)                -- Reduction: remove l
///   multiplyEach     = (*) >> List.map                 -- Composition: (f >> g) x = g (f x)
let multiplyEach = (*) >> List.map

let multiplyEachOriginal x l = List.map (fun y -> y * x) l

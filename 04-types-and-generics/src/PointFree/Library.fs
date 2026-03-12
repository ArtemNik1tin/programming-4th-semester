namespace PointFree

module PointFree =
    let funcPointFree = (*) >> List.map
    let func x l = List.map (fun y -> y * x) l

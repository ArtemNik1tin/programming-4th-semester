module MapForTree

type Tree<'a> =
    | Node of 'a * Tree<'a> * Tree<'a>
    | Empty

type ContinuationStep<'a> =
    | Finished
    | Step of 'a * (unit -> ContinuationStep<'a>)

let rec addNode binTree nodeToAdd =
    match nodeToAdd with
        | Empty -> binTree
        | Node(vertexValue, leftSon, rightSon) ->
            match binTree with
                | Empty -> nodeToAdd
                | Node(x, l, r) ->
                    match x with
                    | x when x < vertexValue -> addNode l nodeToAdd
                    | _ -> addNode r nodeToAdd

let rec linearize binTree cont =
    match binTree with
        | Empty -> cont()
        | Node(x, l, r) -> Step(x, (fun () -> linearize l (fun () -> linearize r cont)))

let map f tree =
    let build tree cont =
        match tree with 
            | Empty -> (Empty, cont)
            | Node(x, l, r) -> (Node(f x, l, r), cont())

let iter f binTree =
    let steps = linearize binTree (fun () -> Finished)
    let rec processSteps step =
        match step with
            | Finished -> ()
            | Step(x, getNext) -> f x processSteps (getNext())
    processSteps steps
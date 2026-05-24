module MapForTree

type Tree<'a> =
    | Node of 'a * Tree<'a> * Tree<'a>
    | Empty

let rec map f tree cont =
    match tree with
    | Empty -> cont Empty
    | Node(x, l, r) ->
        map f l (fun l' ->
            map f r (fun r' ->
                cont (Node(f x, l', r'))
            )
        )

let mapTree f tree = map f tree id

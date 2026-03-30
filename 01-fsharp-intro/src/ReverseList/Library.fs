module ReverseList

let getReverseList inputList =
    let rec getReverseListRecursive resultList currentList =
        match currentList with
        | currentHead :: currentTail -> getReverseListRecursive (currentHead :: resultList) currentTail
        | [] -> resultList

    getReverseListRecursive [] inputList

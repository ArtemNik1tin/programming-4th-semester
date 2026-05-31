namespace PointFree.Tests

open NUnit.Framework
open FsCheck.NUnit
open PointFree

[<TestFixture>]
type PointFreeTests() =

    [<Property>]
    member _.``multiplyEachOriginal should be equivalent to multiplyEachResult`` (x: int) (l: int list) =
        let multiplyEachOriginalResult = multiplyEachOriginal x l
        let multiplyEachResult = multiplyEach x l

        multiplyEachOriginalResult = multiplyEachResult

    [<Property>]
    member _.``multiplyEachOriginal should be equivalent to multiplyEachCommutativity`` (x: int) (l: int list) =
        let multiplyEachOriginalResult = multiplyEachOriginal x l
        let multiplyEachCommutativityResult = multiplyEachCommutativity x l

        multiplyEachOriginalResult = multiplyEachCommutativityResult

    [<Property>]
    member _.``multiplyEachCommutativity should be equivalent to multiplyEachOperatorAsFunction`` (x: int) (l: int list) =
        let multiplyEachCommutativityResult = multiplyEachCommutativity x l
        let multiplyEachOperatorAsFunctionResult = multiplyEachOperatorAsFunction x l

        multiplyEachCommutativityResult = multiplyEachOperatorAsFunctionResult

    [<Property>]
    member _.``multiplyEachOperatorAsFunction should be equivalent to multiplyEachReduction`` (x: int) (l: int list) =
        let multiplyEachOperatorAsFunctionResult = multiplyEachOperatorAsFunction x l
        let multiplyEachReductionResult = multiplyEachReduction x l

        multiplyEachOperatorAsFunctionResult = multiplyEachReductionResult

    [<Property>]
    member _.``multiplyEachReduction should be equivalent to multiplyEachComposition`` (x: int) (l: int list) =
        let multiplyEachReductionResult = multiplyEachReduction x l
        let multiplyEachCompositionResult = multiplyEachComposition x l

        multiplyEachCompositionResult = multiplyEachReductionResult

    [<Property>]
    member _.``multiplyEachComposition should be equivalent to multiplyEach`` (x: int) (l: int list) =
        let multiplyEachCompositionResult = multiplyEachComposition x l
        let multiplyEachResult = multiplyEach x l

        multiplyEachCompositionResult = multiplyEachResult
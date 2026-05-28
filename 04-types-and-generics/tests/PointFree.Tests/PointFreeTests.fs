namespace PointFree.Tests

open NUnit.Framework
open FsCheck.NUnit
open PointFree

[<TestFixture>]
type PointFreeTests() =

    [<Property>]
    member _.``funcPointFree should be equivalent to funcOriginal`` (x: int) (l: int list) =
        let resultOriginal = multiplyEachOriginal x l
        let resultPointFree = multiplyEach x l

        resultOriginal = resultPointFree

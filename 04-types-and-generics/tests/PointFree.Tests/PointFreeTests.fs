namespace PointFree.Tests

open NUnit.Framework
open FsCheck.NUnit
open PointFree

[<TestFixture>]
type PointFreeTests() =

    [<Property>]
    member _.``funcPointFree should be equivalent to funcOriginal`` (x: int) (l: int list) =
        let resultOriginal = PointFree.func x l
        let resultPointFree = PointFree.funcPointFree x l

        resultOriginal = resultPointFree

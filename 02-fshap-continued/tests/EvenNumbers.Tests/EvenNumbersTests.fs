module EvenNumbersTests

open FsUnit
open NUnit.Framework
open EvenNumbers

[<FsCheck.NUnit.Property(MaxTest = 100)>]
let MapImplEqualsFoldImpl (source: list<int>) =
    calculateEvenNumbersUsingMap source = calculateEvenNumbersUsingFold source

[<FsCheck.NUnit.Property(MaxTest = 100)>]
let FoldImplEqualsFilterImpl (source: list<int>) =
    calculateEvenNumbersUsingFold source = calculateEvenNumbersUsingFilter source

[<Test>]
let ``calculateEvenNumbersUsingMap returns 0 for empty list`` () =
    calculateEvenNumbersUsingMap [] |> should equal 0

[<Test>]
let ``calculateEvenNumbersUsingMap returns correct number for base case`` () =
    calculateEvenNumbersUsingMap [ -1; 2; 5; -4; 3 ] |> should equal 2

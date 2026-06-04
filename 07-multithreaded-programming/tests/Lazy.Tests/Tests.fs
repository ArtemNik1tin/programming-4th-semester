module Lazy.Tests

open System.Threading
open System.Threading.Tasks
open NUnit.Framework
open FsUnit
open Lazy

module private Helpers =

    let testGetReturnsValue (create: (unit -> int) -> ILazy<int>) =
        let lazy_ = create (fun () -> 42)
        lazy_.Get() |> should equal 42

    let testRepeatedGetSameValue (create: (unit -> int) -> ILazy<int>) =
        let lazy_ = create (fun () -> 42)
        lazy_.Get() |> should equal 42
        lazy_.Get() |> should equal 42

    let testSupplierCalledOnce (create: (unit -> int) -> ILazy<int>) =
        let calls = ref 0
        let lazy_ = create (fun () ->
            Interlocked.Increment(calls) |> ignore
            42)
        lazy_.Get() |> should equal 42
        lazy_.Get() |> should equal 42
        calls.Value |> should equal 1

    let testReturnsSameObject (create: (unit -> obj) -> ILazy<obj>) =
        let lazy_ = create (fun () -> obj ())
        let v1 = lazy_.Get()
        let v2 = lazy_.Get()
        v2 |> should be (sameAs v1)

    let testMultithreadedConsistency (create: (unit -> int) -> ILazy<int>) =
        let lazy_ = create (fun () -> 42)
        let results = Array.zeroCreate 100
        Parallel.For(0, 100, fun i -> results[i] <- lazy_.Get()) |> ignore
        results |> Array.forall (fun r -> r = 42) |> should equal true

    let testMultithreadedLockedSupplierCalledOnce (create: (unit -> int) -> ILazy<int>) =
        let calls = ref 0
        let lazy_ = create (fun () ->
            Thread.Sleep(10)
            Interlocked.Increment(calls) |> ignore
            42)
        let results = Array.zeroCreate 100
        Parallel.For(0, 100, fun i -> results[i] <- lazy_.Get()) |> ignore
        results |> Array.forall (fun r -> r = 42) |> should equal true
        calls.Value |> should equal 1

    let testMultithreadedLockFreeReturnsSameValue (create: (unit -> int) -> ILazy<int>) =
        let calls = ref 0
        let lazy_ = create (fun () ->
            Thread.Sleep(10)
            Interlocked.Increment(calls) |> ignore
            42)
        let results = Array.zeroCreate 100
        Parallel.For(0, 100, fun i -> results[i] <- lazy_.Get()) |> ignore
        results |> Array.forall (fun r -> r = 42) |> should equal true

open Helpers

[<Test>]
let ``SingleThreadedLazy returns computed value`` () =
    testGetReturnsValue (fun s -> SingleThreadedLazy(s) :> ILazy<_>)

[<Test>]
let ``SingleThreadedLazy repeated Get returns same value`` () =
    testRepeatedGetSameValue (fun s -> SingleThreadedLazy(s) :> ILazy<_>)

[<Test>]
let ``SingleThreadedLazy supplier called once`` () =
    testSupplierCalledOnce (fun s -> SingleThreadedLazy(s) :> ILazy<_>)

[<Test>]
let ``SingleThreadedLazy returns same object`` () =
    testReturnsSameObject (fun s -> SingleThreadedLazy(s) :> ILazy<_>)

[<Test>]
let ``LockedLazy returns computed value`` () =
    testGetReturnsValue (fun s -> LockedLazy(s) :> ILazy<_>)

[<Test>]
let ``LockedLazy repeated Get returns same value`` () =
    testRepeatedGetSameValue (fun s -> LockedLazy(s) :> ILazy<_>)

[<Test>]
let ``LockedLazy supplier called once in single thread`` () =
    testSupplierCalledOnce (fun s -> LockedLazy(s) :> ILazy<_>)

[<Test>]
let ``LockedLazy returns same object`` () =
    testReturnsSameObject (fun s -> LockedLazy(s) :> ILazy<_>)

[<Test>]
let ``LockedLazy multi-threaded consistency`` () =
    testMultithreadedConsistency (fun s -> LockedLazy(s) :> ILazy<_>)

[<Test>]
let ``LockedLazy supplier called once under contention`` () =
    testMultithreadedLockedSupplierCalledOnce (fun s -> LockedLazy(s) :> ILazy<_>)

[<Test>]
let ``LockFreeLazy returns computed value`` () =
    testGetReturnsValue (fun s -> LockFreeLazy(s) :> ILazy<_>)

[<Test>]
let ``LockFreeLazy repeated Get returns same value`` () =
    testRepeatedGetSameValue (fun s -> LockFreeLazy(s) :> ILazy<_>)

[<Test>]
let ``LockFreeLazy supplier called once in single thread`` () =
    testSupplierCalledOnce (fun s -> LockFreeLazy(s) :> ILazy<_>)

[<Test>]
let ``LockFreeLazy returns same object`` () =
    testReturnsSameObject (fun s -> LockFreeLazy(s) :> ILazy<_>)

[<Test>]
let ``LockFreeLazy multi-threaded consistency`` () =
    testMultithreadedConsistency (fun s -> LockFreeLazy(s) :> ILazy<_>)

[<Test>]
let ``LockFreeLazy all threads get same value under contention`` () =
    testMultithreadedLockFreeReturnsSameValue (fun s -> LockFreeLazy(s) :> ILazy<_>)

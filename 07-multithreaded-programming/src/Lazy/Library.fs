namespace Lazy

open System.Threading

/// <summary>
/// Represents a lazy computation that is evaluated on the first call to <c>Get</c>.
/// Subsequent calls return the same value.
/// </summary>
type ILazy<'a> =
    /// <summary>
    /// Returns the (cached) result of the lazy computation.
    /// </summary>
    abstract member Get: unit -> 'a

/// <summary>
/// Single-threaded lazy implementation. No synchronization.
/// Guarantees at-most-one supplier call only in single-threaded scenarios.
/// </summary>
/// <param name="supplier">The factory function for the value.</param>
type SingleThreadedLazy<'a>(supplier: unit -> 'a) =
    let mutable value: Option<'a> = None

    interface ILazy<'a> with
        member _.Get() =
            match value with
            | Some v -> v
            | None ->
                let v = supplier ()
                value <- Some v
                v

/// <summary>
/// Multi-threaded lazy implementation with double-checked locking.
/// Guarantees at-most-one supplier call across all threads.
/// </summary>
/// <param name="supplier">The factory function for the value.</param>
type LockedLazy<'a>(supplier: unit -> 'a) =
    let mutable value: Option<'a> = None
    let lockObj = obj ()

    interface ILazy<'a> with
        member _.Get() =
            match Volatile.Read(&value) with
            | Some v -> v
            | None ->
                lock lockObj (fun () ->
                    match Volatile.Read(&value) with
                    | Some v -> v
                    | None ->
                        let v = supplier ()
                        Volatile.Write(&value, Some v)
                        v)

/// <summary>
/// Lock-free lazy implementation using <c>Interlocked.CompareExchange</c>.
/// Supplier may be called more than once under contention, but <c>Get</c>
/// always returns the same object (extra results are discarded).
/// </summary>
/// <param name="supplier">The factory function for the value.</param>
type LockFreeLazy<'a>(supplier: unit -> 'a) =
    let mutable cached: obj = null

    interface ILazy<'a> with
        member _.Get() =
            match Volatile.Read(&cached) with
            | null ->
                let v = supplier ()
                let result = Interlocked.CompareExchange(&cached, box v, null)
                if isNull result then v else unbox result
            | v -> unbox v

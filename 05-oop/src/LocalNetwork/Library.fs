// <copyright file="Library.fs" company="ArtemNik1tin">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

module LocalNetwork

/// <summary>
/// Interface for random number generation, enabling mocking in tests.
/// </summary>
type IRandomGenerator =
    /// <summary>Returns a random float in [0.0, 1.0).</summary>
    abstract NextDouble: unit -> float

/// <summary>
/// Default random generator backed by <see cref="System.Random" />.
/// </summary>
type DefaultRandomGenerator() =
    let rng = System.Random()
    interface IRandomGenerator with
        member _.NextDouble() = rng.NextDouble()

/// <summary>
/// Represents an operating system with a name and infection probability.
/// </summary>
/// <param name="name">OS display name.</param>
/// <param name="infectionProbability">Probability of infection when exposed (0.0 to 1.0).</param>
type OperatingSystem(name: string, infectionProbability: float) =
    /// <summary>OS display name.</summary>
    member _.Name = name
    /// <summary>Infection probability in [0.0, 1.0].</summary>
    member _.InfectionProbability = infectionProbability

/// <summary>
/// Represents a computer in the local network.
/// </summary>
/// <param name="os">The operating system installed on this computer.</param>
/// <param name="isInitiallyInfected">Whether the computer starts infected.</param>
type Computer(os: OperatingSystem, ?isInitiallyInfected: bool) =
    let mutable infected = defaultArg isInitiallyInfected false
    /// <summary>The operating system installed on this computer.</summary>
    member _.OS = os
    /// <summary>Whether this computer is currently infected.</summary>
    member _.IsInfected = infected
    /// <summary>Infects this computer.</summary>
    member _.Infect() = infected <- true

/// <summary>
/// Simulates virus propagation on a local network.
/// </summary>
type Network private (adjacencyMatrix: bool[,], computers: Computer list, rng: IRandomGenerator) =
    let n = computers.Length

    /// <summary>
    /// Creates a <c>Network</c> after validating the adjacency matrix size.
    /// </summary>
    /// <param name="adjacencyMatrix">N×N matrix where true means a direct connection exists.</param>
    /// <param name="computers">List of computers (length must match matrix dimension).</param>
    /// <param name="rng">Random generator used for infection checks.</param>
    /// <returns><c>Ok Network</c> if valid, <c>Error string</c> otherwise.</returns>
    static member Create(adjacencyMatrix: bool[,], computers: Computer list, rng: IRandomGenerator) =
        let n = computers.Length
        match adjacencyMatrix.GetLength(0) <> n || adjacencyMatrix.GetLength(1) <> n with
        | true -> Error "Adjacency matrix size must equal the number of computers"
        | false -> Ok(Network(adjacencyMatrix, computers, rng))

    /// <summary>
    /// Performs one simulation step. Each non-infected computer with an infected
    /// neighbor may become infected according to its OS infection probability.
    /// </summary>
    /// <returns>true if at least one new infection occurred this step.</returns>
    member _.Step() =
        let previouslyInfected = computers |> List.map _.IsInfected
        let mutable hasNewInfection = false
        for j = 0 to n - 1 do
            match previouslyInfected[j] with
            | true -> ()
            | false ->
                let hasInfectedNeighbor =
                    seq { 0 .. n - 1 }
                    |> Seq.exists (fun i -> adjacencyMatrix[i, j] && previouslyInfected[i])
                match hasInfectedNeighbor with
                | false -> ()
                | true ->
                    match rng.NextDouble() < computers[j].OS.InfectionProbability with
                    | false -> ()
                    | true ->
                        computers[j].Infect()
                        hasNewInfection <- true
        hasNewInfection

    /// <summary>
    /// Runs the simulation until no new infections can occur.
    /// Prints the network state after each step.
    /// </summary>
    member this.Run() =
        let mutable stepNumber = 0
        this.PrintState(stepNumber)
        let mutable changed = true
        while changed do
            stepNumber <- stepNumber + 1
            changed <- this.Step()
            this.PrintState(stepNumber)

    /// <summary>
    /// Prints the current state of all computers to stdout.
    /// </summary>
    /// <param name="step">Current step number (for display).</param>
    member _.PrintState(step: int) =
        printfn $"Step %d{step}:"
        for i = 0 to n - 1 do
            let c = computers[i]
            printfn $"  Computer %d{i}: %s{c.OS.Name}, Infected: %b{c.IsInfected}"
        printfn ""

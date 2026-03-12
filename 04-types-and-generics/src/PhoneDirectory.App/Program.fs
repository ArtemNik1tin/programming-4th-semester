open System
open System.IO
open PhoneDirectory

/// Main recursive loop for the CLI interaction.
let rec mainLoop (directory: PhoneDirectory.Directory) =
    printfn "\n--- PHONE DIRECTORY ---"
    printfn "1. Add Entry"
    printfn "2. Find Phone by Name"
    printfn "3. Find Name by Phone"
    printfn "4. Show All"
    printfn "5. Save to File"
    printfn "6. Load from File"
    printfn "0. Exit"
    printf "Select an option: "

    let input = Console.ReadLine()

    match input with
    | "0" -> printfn "Goodbye!"

    | "1" ->
        printf "Enter Name: "
        let name = Console.ReadLine()
        printf "Enter Phone: "
        let phone = Console.ReadLine()
        let nextDirectory = PhoneDirectory.add name phone directory
        printfn "Entry added."
        mainLoop nextDirectory

    | "2" ->
        printf "Enter Name to search: "
        let name = Console.ReadLine()
        let results = PhoneDirectory.findPhones name directory

        if List.isEmpty results then
            printfn "No records found for '%s'." name
        else
            printfn "Phones found: %s" (String.concat ", " results)

        mainLoop directory

    | "3" ->
        printf "Enter Phone to search: "
        let phone = Console.ReadLine()
        let results = PhoneDirectory.findNames phone directory

        if List.isEmpty results then
            printfn "No records found for phone '%s'." phone
        else
            printfn "Names found: %s" (String.concat ", " results)

        mainLoop directory

    | "4" ->
        printfn "Current Database:"

        if List.isEmpty directory then
            printfn "The directory is empty."
        else
            directory |> List.iter (fun e -> printfn "%s: %s" e.Name e.Phone)

        mainLoop directory

    | "5" ->
        let lines = PhoneDirectory.serialize directory

        try
            File.WriteAllLines("data.txt", lines)
            printfn "Data successfully saved to data.txt."
        with ex ->
            printfn "Error saving file: %s" ex.Message

        mainLoop directory

    | "6" ->
        if File.Exists("data.txt") then
            let lines = File.ReadAllLines("data.txt") |> Array.toList
            let loadedDirectory = PhoneDirectory.deserialize lines
            printfn "Loaded %d entries from file." (List.length loadedDirectory)
            mainLoop loadedDirectory
        else
            printfn "File data.txt not found."
            mainLoop directory

    | _ ->
        printfn "Unknown command, please try again."
        mainLoop directory

[<EntryPoint>]
let main _ =
    let (initialDirectory: PhoneDirectory.Directory) = []
    mainLoop initialDirectory
    0

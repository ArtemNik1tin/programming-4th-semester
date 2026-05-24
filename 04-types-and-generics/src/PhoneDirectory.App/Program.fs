open System
open System.IO
open PhoneDirectory

let showMenu () =
    printfn "\n--- PHONE DIRECTORY ---"
    printfn "Commands:"
    printfn "  add <name> <phone>       - Add entry"
    printfn "  find name <name>         - Find phones by name"
    printfn "  find phone <phone>       - Find names by phone"
    printfn "  all                      - Show all entries"
    printfn "  save                     - Save to file"
    printfn "  load                     - Load from file"
    printfn "  help                     - Show this menu"
    printfn "  exit / quit              - Exit"

let rec mainLoop (directory: Directory) =
    printf "> "
    let input = Console.ReadLine()

    if isNull input then
        printfn "Goodbye!"
    else
        let tokens =
            input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries) |> Array.toList

        match tokens with
        | "exit" :: _ | "quit" :: _ -> printfn "Goodbye!"

        | "add" :: name :: phone :: _ ->
            if name.Contains(";") || phone.Contains(";") then
                printfn "Error: Name and phone must not contain semicolons."
                mainLoop directory
            elif String.IsNullOrWhiteSpace name || String.IsNullOrWhiteSpace phone then
                printfn "Error: Name and phone must not be empty."
                mainLoop directory
            else
                let nextDirectory = add (name.Trim()) (phone.Trim()) directory
                printfn "Entry added."
                mainLoop nextDirectory

        | "add" :: _ ->
            printfn "Usage: add <name> <phone>"
            mainLoop directory

        | "find" :: "name" :: _ ->
            let name = input.Trim().Substring("find name".Length).Trim()

            if String.IsNullOrWhiteSpace name then
                printfn "Usage: find name <name>"
                mainLoop directory
            else
                let results = findPhones name directory

                if List.isEmpty results then
                    printfn $"No records found for '{name}'."
                else
                    results |> List.iter (printfn "  %s")
                mainLoop directory

        | "find" :: "phone" :: _ ->
            let phone = input.Trim().Substring("find phone".Length).Trim()

            if String.IsNullOrWhiteSpace phone then
                printfn "Usage: find phone <phone>"
                mainLoop directory
            else
                let results = findNames phone directory

                if List.isEmpty results then
                    printfn $"No records found for phone '{phone}'."
                else
                    results |> List.iter (printfn "  %s")
                mainLoop directory

        | "all" :: _ ->
            printfn "Current Database:"

            if List.isEmpty directory then
                printfn "  The directory is empty."
            else
                directory |> List.iter (fun e -> printfn $"  {e.Name}: {e.Phone}")

            mainLoop directory

        | "save" :: _ ->
            let lines = serialize directory

            try
                File.WriteAllLines("data.txt", lines)
                printfn "Data successfully saved to data.txt."
            with ex ->
                printfn $"Error saving file: {ex.Message}"

            mainLoop directory

        | "load" :: _ ->
            try
                let lines = File.ReadAllLines("data.txt") |> Array.toList

                match deserialize lines with
                | Ok loadedDirectory ->
                    printfn $"Loaded {List.length loadedDirectory} entries from file."
                    mainLoop loadedDirectory
                | Error err ->
                    printfn $"Error loading file:\n{err}"
                    mainLoop directory
            with ex ->
                printfn $"Error loading file: {ex.Message}"
                mainLoop directory

        | "help" :: _ ->
            showMenu ()
            mainLoop directory

        | _ ->
            printfn "Unknown command. Type 'help' to see available commands."
            mainLoop directory

showMenu ()
mainLoop []

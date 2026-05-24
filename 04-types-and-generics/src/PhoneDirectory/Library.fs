module PhoneDirectory

type Entry = { Name: string; Phone: string }
type Directory = Entry list

let add name phone (directory: Directory) : Directory =
    { Name = name; Phone = phone } :: directory

let findPhones name (directory: Directory) : string list =
    directory |> List.filter (fun e -> e.Name = name) |> List.map _.Phone

let findNames phone (directory: Directory) : string list =
    directory
    |> List.filter (fun e -> e.Phone = phone)
    |> List.map _.Name

let serialize (directory: Directory) : string list =
    directory |> List.map (fun e -> $"%s{e.Name};%s{e.Phone}")

let deserialize (lines: string list) : Result<Directory, string> =
    let rec loop lines acc errors =
        match lines with
        | [] ->
            match errors with
            | [] -> Ok (List.rev acc)
            | _ -> Error (String.concat "\n" errors)
        | line :: rest ->
            if System.String.IsNullOrWhiteSpace line then
                loop rest acc errors
            else
                let parts = line.Split(';')

                match parts with
                | [| name; phone |] when
                    not (System.String.IsNullOrWhiteSpace name)
                    && not (System.String.IsNullOrWhiteSpace phone)
                    ->
                    loop
                        rest
                        ({ Name = name.Trim()
                           Phone = phone.Trim() }
                          :: acc)
                        errors
                | _ -> loop rest acc ($"Invalid line: {line}" :: errors)

    loop lines [] []

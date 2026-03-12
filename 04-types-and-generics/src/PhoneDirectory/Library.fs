namespace PhoneDirectory

module PhoneDirectory =
    type Entry = { Name: string; Phone: string }
    type Directory = Entry list

    let add name phone (directory: Directory) : Directory =
        { Name = name; Phone = phone } :: directory

    let findPhones name (directory: Directory) : string list =
        directory |> List.filter (fun e -> e.Name = name) |> List.map (fun e -> e.Phone)

    let findNames phone (directory: Directory) : string list =
        directory
        |> List.filter (fun e -> e.Phone = phone)
        |> List.map (fun e -> e.Name)

    let serialize (directory: Directory) : string list =
        directory |> List.map (fun e -> sprintf "%s;%s" e.Name e.Phone)

    let deserialize (lines: string list) : Directory =
        lines
        |> List.choose (fun line ->
            let parts = line.Split(';')

            match parts with
            | [| name; phone |] when
                not (System.String.IsNullOrWhiteSpace name)
                && not (System.String.IsNullOrWhiteSpace phone)
                ->
                Some
                    { Name = name.Trim()
                      Phone = phone.Trim() }
            | _ -> None)

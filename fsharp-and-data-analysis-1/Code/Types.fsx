type Mixed = JsonProvider<""" [1, 2, "hello", "world"] """>
let mixed = Mixed.Parse(""" [4, 5, "hello", "world" ] """)

mixed.Numbers |> Seq.sum
mixed.Strings |> String.concat ", "
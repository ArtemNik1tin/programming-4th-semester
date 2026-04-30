let groupedTrace1 =
    Bar(
        x = ["giraffes"; "orangutans"; "monkeys"],
        y = [20; 14; 23],
        name= "SF Zoo")
let groupedTrace2 =
    Bar(
        x = ["giraffes"; "orangutans"; "monkeys"],
        y = [12; 18; 29],
        name = "LA Zoo")

let groupedLayout = Layout(barmode = "group")

let chart2 =
    [groupedTrace1; groupedTrace2]
    |> Chart.Plot
    |> Chart.WithLayout groupedLayout
    |> Chart.WithHeight 500
    |> Chart.WithWidth 700
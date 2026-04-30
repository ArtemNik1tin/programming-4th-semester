open XPlot.Plotly

let layout = Layout(title = "Basic Bar Chart")

let data = ["giraffes", 20; "orangutans", 14; "monkeys", 23]

let chart1 =
    data
    |> Chart.Bar
    |> Chart.WithLayout layout
    |> Chart.WithHeight 500
    |> Chart.WithWidth 700
open FSharp.Data

let apiUrl = "https://api.openweathermap.org/data/2.5/weather?q="

type Weather = JsonProvider<sample>

let sf = Weather.Load(apiUrl + "Saint Petersburg")

sf.Sys.Country
sf.Main.Temp

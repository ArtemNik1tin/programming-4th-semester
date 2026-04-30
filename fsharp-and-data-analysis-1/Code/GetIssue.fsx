// GitHub.json downloaded from
// https://api.github.com/repos/fsharp/FSharp.Data/issues
type GitHub = JsonProvider<
    "../data/GitHub.json",
    ResolutionFolder=ResolutionFolder
>

let topRecentlyUpdatedIssues =
    GitHub.GetSamples()
    |> Seq.filter (fun issue -> issue.State = "open")
    |> Seq.sortBy (fun issue -> System.DateTimeOffset.Now - issue.UpdatedAt)
    |> Seq.truncate 2

for issue in topRecentlyUpdatedIssues do
    printfn "#%d %s" issue.Number issue.Title

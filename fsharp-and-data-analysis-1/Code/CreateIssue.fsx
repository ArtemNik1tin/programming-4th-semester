type GitHubIssue = JsonProvider<issueSample, RootName="issue">

let newIssue =
    GitHubIssue.Issue(
        "Test issue",
        "This is a test issue created in FSharp.Data documentation",
        assignee = "",
        labels = [||],
        milestone = 0
    )

newIssue.JsonValue.Request
    "https://api.github.com/repos/fsharp/FSharp.Data/issues"
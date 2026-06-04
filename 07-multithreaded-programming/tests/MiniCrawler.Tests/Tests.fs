module MiniCrawler.Tests

open NUnit.Framework
open FsUnit
open MiniCrawler

[<Test>]
let ``extractLinks finds a single http link`` () =
    let html = "<a href=\"http://example.com\">link</a>"
    extractLinks html |> should equal [ "http://example.com" ]

[<Test>]
let ``extractLinks finds https links`` () =
    let html = "<a href=\"https://example.com\">secure</a>"
    extractLinks html |> should equal [ "https://example.com" ]

[<Test>]
let ``extractLinks finds multiple links`` () =
    let html =
        "<a href=\"http://a.com\">A</a> <a href=\"http://b.com\">B</a>"
    extractLinks html |> should equal [ "http://a.com"; "http://b.com" ]

[<Test>]
let ``extractLinks deduplicates identical URLs`` () =
    let html =
        "<a href=\"http://x.com\">X</a><a href=\"http://x.com\">X again</a>"
    extractLinks html |> should equal [ "http://x.com" ]

[<Test>]
let ``extractLinks ignores relative hrefs`` () =
    let html = "<a href=\"/relative\">rel</a> <a href=\"http://abs.com\">abs</a>"
    extractLinks html |> should equal [ "http://abs.com" ]

[<Test>]
let ``extractLinks returns empty for no links`` () =
    extractLinks "<html><body>no anchors</body></html>" |> should be Empty

[<Test>]
let ``extractLinks returns empty for empty string`` () =
    extractLinks "" |> should be Empty

[<Test>]
let ``extractLinks ignores anchor tags without href`` () =
    let html = "<a name=\"anchor\">named</a>"
    extractLinks html |> should be Empty

[<Test>]
let ``extractLinks handles extra whitespace around href`` () =
    let html = "<a  href  =  \"http://spaced.com\" >link</a>"
    extractLinks html |> should equal [ "http://spaced.com" ]

[<Test>]
let ``extractLinks is case-insensitive for tag and attribute`` () =
    let html = "<A HREF=\"http://upcase.com\">UP</A>"
    extractLinks html |> should equal [ "http://upcase.com" ]

[<Test>]
let ``extractLinks handles real-world-ish HTML snippet`` () =
    let html =
        """<html>
<body>
<p>Links: <a href="http://first.com">first</a></p>
<p>Also <a href="https://second.com/path?q=1">second</a></p>
</body>
</html>"""
    extractLinks html
    |> should equal [ "http://first.com"; "https://second.com/path?q=1" ]

[<Test>]
let ``crawlWith returns sizes of linked pages`` () =
    let startHtml = """<a href="http://a.com">A</a><a href="http://b.com">B</a>"""
    let fakeDownload url =
        match url with
        | "http://start.com" -> async { return startHtml }
        | "http://a.com" -> async { return "hello" }
        | "http://b.com" -> async { return "world!!!" }
        | _ -> async { return "" }

    let results =
        crawlWith fakeDownload "http://start.com"
        |> Async.RunSynchronously
    results |> should contain ("http://a.com", 5)
    results |> should contain ("http://b.com", 8)
    results |> should haveLength 2

[<Test>]
let ``crawlWith returns empty when start page has no links`` () =
    let results =
        crawlWith (fun _ -> async { return "<html></html>" }) "http://start.com"
        |> Async.RunSynchronously
    results |> should be Empty

[<Test>]
let ``crawlWith skips failed downloads`` () =
    let startHtml = """<a href="http://good.com">good</a><a href="http://bad.com">bad</a>"""
    let fakeDownload url =
        match url with
        | "http://start.com" -> async { return startHtml }
        | "http://good.com" -> async { return "data" }
        | "http://bad.com" -> async { return failwith "timeout" }
        | _ -> async { return "" }

    let results =
        crawlWith fakeDownload "http://start.com"
        |> Async.RunSynchronously
    results |> should equal [ "http://good.com", 4 ]

[<Test>]
let ``crawlWith returns empty when start page download fails`` () =
    let fakeDownload _ =
        async { return failwith "error" }

    let results =
        crawlWith fakeDownload "http://start.com"
        |> Async.RunSynchronously
    results |> should be Empty

[<Test>]
let ``crawlWith handles empty link list from start page`` () =
    let fakeDownload _ = async { return "" }

    let results =
        crawlWith fakeDownload "http://start.com"
        |> Async.RunSynchronously
    results |> should be Empty

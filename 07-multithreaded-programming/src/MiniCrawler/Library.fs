module MiniCrawler

open System
open System.Net.Http
open System.Text.RegularExpressions

/// <summary>
/// Extracts all http/https href values from anchor tags in HTML.
/// Only matches <c>&lt;a href="http://..."&gt;</c> (double-quoted, http/https scheme).
/// </summary>
/// <param name="html">The HTML content to scan.</param>
/// <returns>List of distinct absolute URLs found.</returns>
let extractLinks (html: string) : string list =
    let pattern = @"<a\s+[^>]*href\s*=\s*""(https?://[^""]+)""[^>]*>"
    let matches = Regex.Matches(html, pattern, RegexOptions.IgnoreCase)
    [ for m in matches -> m.Groups[1].Value ]
    |> List.distinct

/// <summary>
/// Crawls the page at <paramref name="startUrl"/> using a caller-provided
/// download function. Returns a list of successfully fetched linked pages
/// with their character counts.
/// </summary>
/// <param name="download">Function that returns page content for a URL.</param>
/// <param name="startUrl">The URL of the starting page.</param>
/// <returns>List of (url, charCount) for linked pages that were downloaded.</returns>
let crawlWith (download: string -> Async<string>) (startUrl: string) : Async<(string * int) list> = async {
    let! html = async {
        try
            return! download startUrl
        with _ -> return ""
    }

    let links = extractLinks html

    let! results =
        links
        |> List.map (fun url -> async {
            try
                let! content = download url
                return Some (url, content.Length)
            with _ -> return None
        })
        |> Async.Parallel

    return
        results
        |> Array.choose id
        |> Array.toList
}

/// <summary>
/// Crawls the page at <paramref name="startUrl"/>: downloads it, extracts all
/// http/https links, downloads every linked page in parallel, and prints
/// <c>"url — charCount"</c> for each successfully fetched page.
/// </summary>
/// <param name="startUrl">The URL of the starting page.</param>
let crawl (startUrl: string) : Async<unit> = async {
    use client = new HttpClient()
    let download (url: string) = async {
        return! client.GetStringAsync(url) |> Async.AwaitTask
    }

    let! results = crawlWith download startUrl

    for url, size in results do
        printfn $"%s{url} — %d{size}"
}

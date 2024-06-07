using DynamicData;
using Dynamo_Desktop.Models.Anime;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dynamo_Desktop.Services;

namespace Dynamo_Desktop.Scrapers.Anime;

public partial class AnimePaheScraper
{
    private HttpClient _http = new HttpClient();
    private string Host = SettingsService.Settings().Providers.animepahe.host;
    public async Task<string> RecentAnime(int Page = 1)
    {  
        List<PopularAnime> recent = new();
        var handler = new HttpClientHandler();
        handler.UseCookies = false;

        handler.AutomaticDecompression = ~DecompressionMethods.None; 
        using (var httpClient = new HttpClient(handler))
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{Host}/api?m=airing&page={Page}"))
            {
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:126.0) Gecko/20100101 Firefox/126.0");
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/javascript, */*; q=0.01");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br, zstd");
                request.Headers.TryAddWithoutValidation("Referer", "https://animepahe.ru/");
                request.Headers.TryAddWithoutValidation("X-Requested-With", "XMLHttpRequest");
                request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "empty");
                request.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "cors");
                request.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "same-origin");
                request.Headers.TryAddWithoutValidation("TE", "trailers");
                request.Headers.TryAddWithoutValidation("Cookie", "__ddgid_=dvB6IyowCIdclQM3; __ddg2_=Jk4UTRNXo2Ew2InP; __ddg1_=mrU8HTW5YbTLmgllwMVd;"); 

                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = JsonSerializer.Deserialize<AnimePaheRecentEpisodesJsonResponse>(await response.Content.ReadAsStringAsync());
                    foreach (var data in responseData.data)
                    {
                        recent.Add(new()
                        {
                            Title = data.anime_title,
                            AnimeId = data.anime_session,
                            Episode = data.episode,
                            Image =  data.snapshot
                        });
                    }
                }
            }
        }
        return JsonSerializer.Serialize(recent);
    }
        public async Task<string> AnimeInfo(string Query)
        {
            var animeDetails = new AnimeInfo();
            var handler = new HttpClientHandler();
            handler.UseCookies = false;
            handler.AutomaticDecompression = ~DecompressionMethods.None;
            string url = $"{Host}/anime/{Query}";

            using (var httpClient = new HttpClient(handler))
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                {
                    request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:126.0) Gecko/20100101 Firefox/126.0");
                    request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
                    request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                    request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br, zstd");
                    request.Headers.TryAddWithoutValidation("Referer", Host);
                    request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                    request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
                    request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "document");
                    request.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "navigate");
                    request.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "same-origin");
                    request.Headers.TryAddWithoutValidation("Priority", "u=1");
                    request.Headers.TryAddWithoutValidation("Cookie", "__ddgid_=dvB6IyowCIdclQM3; __ddg2_=Jk4UTRNXo2Ew2InP; __ddg1_=mrU8HTW5YbTLmgllwMVd; res=720; aud=jpn; av1=0;");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        HtmlDocument htmlDoc = new HtmlDocument();
                        htmlDoc.LoadHtml(await response.Content.ReadAsStringAsync());

                        var animePoster = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='anime-poster']");
                        animeDetails.Image = animePoster.SelectSingleNode(".//a").GetAttributeValue("href", null);

                        animeDetails.Description = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='anime-synopsis']").InnerText;

                        var animeInfo = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class,'anime-info')]");
                        animeDetails.Title = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='title-wrapper']/h1/span").InnerText;
                        animeDetails.Episodes = new();
                        using (var request2 = new HttpRequestMessage(new HttpMethod("GET"), $"{Host}/api?m=release&id={Query}&sort=episode_asc&page=1"))
                        {
                            request2.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:126.0) Gecko/20100101 Firefox/126.0");
                            request2.Headers.TryAddWithoutValidation("Accept", "application/json, text/javascript, */*; q=0.01");
                            request2.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                            request2.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br, zstd");
                            request2.Headers.TryAddWithoutValidation("Referer", url);
                            request2.Headers.TryAddWithoutValidation("X-Requested-With", "XMLHttpRequest");
                            request2.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                            request2.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "empty");
                            request2.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "cors");
                            request2.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "same-origin");
                            request2.Headers.TryAddWithoutValidation("Cookie", "__ddgid_=dvB6IyowCIdclQM3; __ddg2_=Jk4UTRNXo2Ew2InP; __ddg1_=mrU8HTW5YbTLmgllwMVd; res=720; aud=jpn; av1=0;");

                            var response2 = await httpClient.SendAsync(request2);
                            if (response2.IsSuccessStatusCode)
                            {
                                var response2body = JsonSerializer.Deserialize<AnimePaheEpisodesJsonResponse>(await response2.Content.ReadAsStringAsync());
                                int totalEpisodes = response2body.total;
                                for (int i = 1; i <= totalEpisodes; i++)
                                {
                                    animeDetails.Episodes.Add(new()
                                    {
                                        EpisodeNumber = i,
                                    });
                                }
                            }
                        }
                    }
                }
            }

            return JsonSerializer.Serialize(animeDetails);
        }
        
    public async Task<string> Search(String Query = "",int Page = 1)
    {
        List<PopularAnime> searchResults = new();
        var handler = new HttpClientHandler();
        handler.UseCookies = false;
        handler.AutomaticDecompression = ~DecompressionMethods.None; 
        using (var httpClient = new HttpClient(handler))
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{Host}/api?m=search&q={Query}"))
            {
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:126.0) Gecko/20100101 Firefox/126.0");
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/javascript, */*; q=0.01");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br, zstd");
                request.Headers.TryAddWithoutValidation("Referer", $"{Host}");
                request.Headers.TryAddWithoutValidation("X-Requested-With", "XMLHttpRequest");
                request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "empty");
                request.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "cors");
                request.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "same-origin");
                request.Headers.TryAddWithoutValidation("TE", "trailers");
                request.Headers.TryAddWithoutValidation("Cookie", "__ddgid_=dvB6IyowCIdclQM3; __ddg2_=Jk4UTRNXo2Ew2InP; __ddg1_=mrU8HTW5YbTLmgllwMVd; res=720; aud=jpn; av1=0;"); 
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody =
                        JsonSerializer.Deserialize<AnimePaheSearchJsonResponse>(
                            await response.Content.ReadAsStringAsync());
                    foreach (var data in responseBody.data)
                    {
                        searchResults.Add(new()
                        {
                            Title = data.title,
                            Image = data.poster,
                            AnimeId = data.session,
                        });
                    }
                }
            }
        }
        return JsonSerializer.Serialize(searchResults);
    }
    public async Task<string> EpisodeStreamLinks(string AnimeId, int Episode)
{
    var handler = new HttpClientHandler();
    handler.UseCookies = false;
    handler.AutomaticDecompression = ~DecompressionMethods.None;
    bool foundEpisode = false;

    while (!foundEpisode)
    {
        using (var httpClient = new HttpClient(handler))
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{Host}/api?m=release&id={AnimeId}&sort=episode_asc&page=1"))
            {
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:126.0) Gecko/20100101 Firefox/126.0");
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/javascript, */*; q=0.01");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br, zstd");
                request.Headers.TryAddWithoutValidation("Referer", "https://animepahe.ru/anime/25e68f23-0686-9946-81be-755adce081b3");
                request.Headers.TryAddWithoutValidation("X-Requested-With", "XMLHttpRequest");
                request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "empty");
                request.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "cors");
                request.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "same-origin");
                request.Headers.TryAddWithoutValidation("TE", "trailers");
                request.Headers.TryAddWithoutValidation("Cookie", "__ddgid_=dvB6IyowCIdclQM3; __ddg2_=Jk4UTRNXo2Ew2InP; __ddg1_=mrU8HTW5YbTLmgllwMVd;");

                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = JsonSerializer.Deserialize<AnimePaheEpisodesJsonResponse>(await response.Content.ReadAsStringAsync());
                    foreach (var data in responseData.data)
                    {
                        if (data.episode == Episode)
                        {
                            foundEpisode = true;
                            string EpisodeId = data.session;
                            // continue
                            string url = $"{Host}/play/{AnimeId}/{EpisodeId}";
                            using (var request2 = new HttpRequestMessage(new HttpMethod("GET"), url))
                            {
                                request2.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:126.0) Gecko/20100101 Firefox/126.0");
                                request2.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
                                request2.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                                request2.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br, zstd");
                                request2.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                                request2.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
                                request2.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "document");
                                request2.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "navigate");
                                request2.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "cross-site");
                                request2.Headers.TryAddWithoutValidation("Priority", "u=1");
                                request2.Headers.TryAddWithoutValidation("Cookie", "__ddgid_=dvB6IyowCIdclQM3; __ddg2_=Jk4UTRNXo2Ew2InP; __ddg1_=mrU8HTW5YbTLmgllwMVd; res=720; aud=jpn; av1=0;");
                                
                                var response2 = await httpClient.SendAsync(request2);
                                HtmlDocument htmlDoc = new HtmlDocument();
                                if (!response2.IsSuccessStatusCode)
                                {
                                    return default;
                                }
                                htmlDoc.LoadHtml(await response2.Content.ReadAsStringAsync());

                                HtmlNode resolutionMenu = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='resolutionMenu']");
                                List<AnimeStreamingLinks> Streaming_links = new List<AnimeStreamingLinks>();
                                foreach (var button in resolutionMenu.SelectNodes(".//button"))
                                {
                                    AnimeStreamingLinks StreamingLink = new();
                                    StreamingLink.Quality = $"{button.GetAttributeValue("data-resolution", null)}-{button.GetAttributeValue("data-audio", null)}";
                                    string kwik_server = button.GetAttributeValue("data-src", null);
                                    // third HTTP request
                                    using (var request3 = new HttpRequestMessage(new HttpMethod("GET"), kwik_server))
                                    {
                                        request3.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:126.0) Gecko/20100101 Firefox/126.0");
                                        request3.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
                                        request3.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                                        request3.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br, zstd");
                                        request3.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                                        request3.Headers.TryAddWithoutValidation("Referer", Host);
                                        request3.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
                                        request3.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "iframe");
                                        request3.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "navigate");
                                        request3.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "cross-site");
                                        request3.Headers.TryAddWithoutValidation("Priority", "u=4");
                                        request3.Headers.TryAddWithoutValidation("TE", "trailers");
                                        request3.Headers.TryAddWithoutValidation("Cookie", "kwik_session=eyJpdiI6ImRRRjJZaUduUjdDMHlyOUpTL3RNSnc9PSIsInZhbHVlIjoiRTFOK0NxcVhaSk9pN2R6T3lSVENMcDJWT1VCR0dEZjV1cWpuRGN5NHU3MEdxdlJ0YmJBWGVGd2YwWWlpZU83RFJXK1FMSzR0YTNPR3dpWDdvMFBuVUpzZklFekJxdHpEUDJHYithSWY3c3pSVVRJeWFIc1NPM2dyVjhPbGk0RmoiLCJtYWMiOiIzZGE3ZDljMmM0N2ZjOGNlYmUzZGM1NTU5NWQyZTYzNTRkNTA4ODZmY2JhYWI3ZDlmMjI1NzliMGYyNjE5OThhIiwidGFnIjoiIn0%3D; srv=s0; cf_clearance=4TOifZu8T69LCqWPALPreQjuW0tDVJLmh6kco1uvRsw-1717744831-1.0.1.1-E1RMRA8w884gs6RHSqBRc40T9EvTudh66.ciKoPuI2BFDNst9rdD1kMDz_tLqtUL52mJw4lFCSCQX7Ws7K3FVA");

                                        var response3 = await httpClient.SendAsync(request3);
                                        HtmlDocument kwik_body = new HtmlDocument();
                                        kwik_body.LoadHtml(await response3.Content.ReadAsStringAsync());
                                        int number_of_script_tags = kwik_body.DocumentNode.SelectNodes("//script").Count;
                                        var target_script = kwik_body.DocumentNode.SelectNodes("//script")[number_of_script_tags - 2];
                                    
                                        List<string> data_list = target_script.InnerText.Split("'player||")[1].Split("|").ToList();
                                        string path = data_list[92] == "stream" ? "stream" : "hls";
                                        string title = "https:" + kwik_body.DocumentNode.SelectNodes("//link[@rel='preconnect']")[1].GetAttributeValue("href", null);
                                        string m3u8_link = title + $"/{path}" + $"/{title.Split("-")[1].Split(".")[0]}" + $"/{data_list[91]}" + $"/{data_list[90]}" + $"/{data_list[89]}" + $".{data_list[88]}";
                                        StreamingLink.Source = m3u8_link;
                                        Streaming_links.Add(StreamingLink);
                                    }
                                }
                             //   Debug.WriteLine(JsonSerializer.Serialize(Streaming_links));
                                return JsonSerializer.Serialize(Streaming_links);
                            }
                        }
                    }
                }
            }
        }
    }

    return default;
}

    
}

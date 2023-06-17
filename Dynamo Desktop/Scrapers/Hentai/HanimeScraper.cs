using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Dynamo_Desktop.Models.Hentai;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Dynamo_Desktop.Scrapers.Hentai;

public class HanimeScraper
{
    private HttpClient _http = new HttpClient();

    public async Task<string> Recent()
    {
        string url = "https://hanime.tv/";
        string response = await _http.GetStringAsync(url);
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response);
        HtmlNodeCollection script_tags = htmlDoc.DocumentNode.SelectNodes("//script");
        var long_scripts = script_tags.Where(tag => tag.InnerText.Length > 2000)
            .Select(tag => tag.InnerHtml)
            .ToList();
        string nuxt_json = long_scripts[0].Split("window.__NUXT__=")[1].TrimEnd(";".ToCharArray());
        //state.data.landing.hentai_videos
        var json_object = JObject.Parse(nuxt_json);
        var hentai_videos = json_object.SelectToken("state.data.landing.hentai_videos").ToList();
        List<RecentHentai> recent_hentai = new List<RecentHentai>();
        foreach (JToken token in hentai_videos)
        {
            RecentHentai hentai = new RecentHentai();
            hentai.HentaiId = token.SelectToken("slug").ToString();
            hentai.Title = token.SelectToken("name").ToString();
            hentai.Image = token.SelectToken("poster_url").ToString();
            recent_hentai.Add(hentai);
        }

        return System.Text.Json.JsonSerializer.Serialize(recent_hentai);
    }

    public async Task<string> Info(string HentaiId)
    {
        string url = $"https://hanime.tv/videos/hentai/{HentaiId}";
        string response = await _http.GetStringAsync(url);
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response);
        HtmlNodeCollection script_tags = htmlDoc.DocumentNode.SelectNodes("//script");
        var long_scripts = script_tags.Where(tag => tag.InnerText.Length > 2000)
            .Select(tag => tag.InnerHtml)
            .ToList();
        string nuxt_json = long_scripts[0].Split("window.__NUXT__=")[1].TrimEnd(";".ToCharArray());
        var json_object = JObject.Parse(nuxt_json);
        var video = json_object.SelectToken("state.data.video.hentai_video");
        Models.Hentai.Hentai hentai = new Models.Hentai.Hentai
        {
            Title = video.SelectToken("name").ToString(),
            Description = video.SelectToken("description").ToString(),
            Views = int.Parse(video.SelectToken("views").ToString()),
            poster = video.SelectToken("poster_url").ToString(),
            Tags = new List<string>()
        };
        var hentai_tags = video.SelectToken("hentai_tags");
        foreach (var hentai_tag in hentai_tags.ToList())
        {
            hentai.Tags.Add(hentai_tag.SelectToken("text").ToString());
        }

        return JsonSerializer.Serialize(hentai);
    }

    public async Task<string> Search(string Query)
    {
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://search.htv-services.com/"))
            {
                request.Headers.TryAddWithoutValidation("authority", "search.htv-services.com");
                request.Headers.TryAddWithoutValidation("accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("accept-language", "en-US,en;q=0.9");
                request.Headers.TryAddWithoutValidation("origin", "https://hanime.tv");
                request.Headers.TryAddWithoutValidation("sec-ch-ua",
                    "\"Microsoft Edge\";v=\"113\", \"Chromium\";v=\"113\", \"Not-A.Brand\";v=\"24\"");
                request.Headers.TryAddWithoutValidation("sec-ch-ua-mobile", "?0");
                request.Headers.TryAddWithoutValidation("sec-ch-ua-platform", "\"Windows\"");
                request.Headers.TryAddWithoutValidation("sec-fetch-dest", "empty");
                request.Headers.TryAddWithoutValidation("sec-fetch-mode", "cors");
                request.Headers.TryAddWithoutValidation("sec-fetch-site", "cross-site");
                request.Headers.TryAddWithoutValidation("user-agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36 Edg/113.0.1774.42");
                string payload =
                    $"{{\"search_text\":\"{Query}\",\"tags\":[],\"tags_mode\":\"AND\",\"brands\":[],\"blacklist\":[],\"order_by\":\"created_at_unix\",\"ordering\":\"desc\",\"page\":0}}";
                request.Content =
                    new StringContent(payload);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;charset=UTF-8");
                var response = await httpClient.SendAsync(request);
                string json_response = await response.Content.ReadAsStringAsync();
                List<RecentHentai> hentai_search = new List<RecentHentai>();
                JObject result = JObject.Parse(json_response);
                var hits = result.SelectToken("hits");
                JArray hits_array = JArray.Parse((hits.ToString()));
                foreach (var hit in hits_array.ToList())
                {
                    
                    RecentHentai hentai = new RecentHentai();
                    hentai.HentaiId = hit.SelectToken("slug").ToString();
                    hentai.Image = hit.SelectToken("cover_url").ToString();
                    hentai.Title = hit.SelectToken("name").ToString();
                    hentai_search.Add(hentai);
                }
             return JsonSerializer.Serialize(hentai_search);
            }
        }
    }
}
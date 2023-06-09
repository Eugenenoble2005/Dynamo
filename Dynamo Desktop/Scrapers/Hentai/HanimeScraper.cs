using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Dynamo_Desktop.Models.Hentai;

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
        foreach(JToken token in hentai_videos)
        {
            RecentHentai hentai = new RecentHentai();
            hentai.HentaiId = token.SelectToken("slug").ToString();
            hentai.Title = token.SelectToken("name").ToString();
            hentai.Image = token.SelectToken("poster_url").ToString();
            recent_hentai.Add(hentai);
        }
        return System.Text.Json.JsonSerializer.Serialize(recent_hentai);
    }
}

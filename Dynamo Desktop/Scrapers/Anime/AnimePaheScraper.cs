using DynamicData;
using Dynamo_Desktop.Models.Anime;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Scrapers.Anime;

public  class AnimePaheScraper
{
    private HttpClient _http = new HttpClient();
    public async Task<string> AnimeInfo(String Id)
    {
        /**Pray this doesn't  break randomly*/
        var AnimeDetails = new AnimePaheAnimeInfo();
        string url = $"https://animepahe.com/anime/{Id}";
        string response = await _http.GetStringAsync(url);
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response);

        var anime_poster = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='anime-poster']");
        AnimeDetails.CoverImage = anime_poster.SelectSingleNode(".//a").GetAttributeValue("href", null);

        AnimeDetails.Description = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='anime-synopsis']")
            .InnerText;

        var anime_info = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class,'anime-info')]");
        AnimeDetails.Title = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='title-wrapper']/h1/span").InnerText;
        var pElements = anime_info.SelectNodes(".//p");
        foreach(var pElement in pElements)
        {
            string title = pElement.InnerText.Split(":")[0];
            string value = pElement.InnerText.Split(":")[1].Trim();
            switch (title.Trim().ToLower())
            {
                case "japanese":
                    AnimeDetails.JapaneseName = value;
                    break;
                case "type":
                    AnimeDetails.Type = value;
                    break;
                case "episodes":
                    AnimeDetails.EpisodeCount =  value;
                    break;
                case "status":
                    AnimeDetails.AnimeStatus = value;
                    break;
                case "duration":
                    AnimeDetails.Duration = value;
                    break;
                case "aired":
                    AnimeDetails.AirRange = value;
                    break;
                case "season":
                    AnimeDetails.Season = value;
                    break;
                case "studio":
                    AnimeDetails.Studio = value;
                    break;
                case "themes":
                    AnimeDetails.Themes = value;
                    break;
                case "theme":
                    AnimeDetails.Themes = value;
                    break;
            }
        }
       
        return JsonSerializer.Serialize(AnimeDetails);
    }

    public async void EpisodeStreamLinks(String AnimeId,string EpisodeId)
    {
        string url = $"https://animepahe.com/play/{AnimeId}/{EpisodeId}";
        string response = await _http.GetStringAsync(url);
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response);

        HtmlNode resolutionMenu = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='resolutionMenu']");
        _http.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.54 Safari/537.36"); // Replace with an appropriate User-Agent value
        _http.DefaultRequestHeaders.Add("Referer",url);
        foreach (var button in resolutionMenu.SelectNodes(".//button"))
        {
            string kwik_server = button.GetAttributeValue("data-src", null);
            var kwik_response = await _http.GetStringAsync(kwik_server);
            HtmlDocument kwik_body = new HtmlDocument();
        }
    }
}

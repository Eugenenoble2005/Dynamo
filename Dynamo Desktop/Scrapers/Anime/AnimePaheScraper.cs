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
            kwik_body.LoadHtml(kwik_response);
            int number_of_script_tags = kwik_body.DocumentNode.SelectNodes("//script").Count;
            var target_script = kwik_body.DocumentNode.SelectNodes("//script")[number_of_script_tags - 2];
            List<string> data_list = target_script.InnerText.Split("'player||")[1].Split("|").ToList();
            //https://eu-99.files.nextcdn.org/stream/99/01/8b08d64d96c9d6978dee6cb90dc289905517ae932f301c8a71c2e08e7719845f/uwu.m3u8
            string title = "https:"+kwik_body.DocumentNode.SelectNodes("//link[@rel='preconnect']")[1].GetAttributeValue("href",null);
            string m3u8_link = title + "/stream" + $"/{title.Split("-")[1].Split(".")[0]}" + "/01" + $"/{data_list[90]}" + $"/{data_list[89]}" +
                               $".{data_list[88]}";
           // Debug.WriteLine(m3u8_link);
            for (int i = 0; i <= data_list.Count-1; i++)
            {
                if (i == 2)
                {
                     Debug.WriteLine(i);
                     Debug.WriteLine(data_list[i]);
                }
               
            }
        }
    }
}

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

    public async Task<string> EpisodeStreamLinks(String AnimeId,string EpisodeId)
    {
        string url = $"https://animepahe.com/play/{AnimeId}/{EpisodeId}";
        string response = await _http.GetStringAsync(url);
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response);

        HtmlNode resolutionMenu = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='resolutionMenu']");
        _http.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.54 Safari/537.36"); // Replace with an appropriate User-Agent value
        //remove previous referer before adding new one
        _http.DefaultRequestHeaders.Remove("Referer");
        _http.DefaultRequestHeaders.Add("Referer",url);
        List<AnimePaheStreamingLinks> Streaming_links = new List<AnimePaheStreamingLinks>();
        foreach (var button in resolutionMenu.SelectNodes(".//button"))
        {
            AnimePaheStreamingLinks StreamingLink = new AnimePaheStreamingLinks();
            StreamingLink.Audio = button.GetAttributeValue("data-audio", null);
            StreamingLink.Fansub = button.GetAttributeValue("data-fansub", null);
            StreamingLink.Resolution = button.GetAttributeValue("data-resolution", null);
            string kwik_server = button.GetAttributeValue("data-src", null);
            var kwik_response = await _http.GetStringAsync(kwik_server);
            HtmlDocument kwik_body = new HtmlDocument();
            kwik_body.LoadHtml(kwik_response);
            int number_of_script_tags = kwik_body.DocumentNode.SelectNodes("//script").Count;
            var target_script = kwik_body.DocumentNode.SelectNodes("//script")[number_of_script_tags - 2];
            List<string> data_list = target_script.InnerText.Split("'player||")[1].Split("|").ToList();
            string path = data_list[92] == "stream"  ? "stream"  : "hls";
            //https://eu-11.cache.nextcdn.org/hls/11/03/0c4a79d85fbdbadc8e7ac99d84c581bfabedebf668f7b9c435e4a27cff3abcff/owo.m3u8
            string title = "https:"+kwik_body.DocumentNode.SelectNodes("//link[@rel='preconnect']")[1].GetAttributeValue("href",null);
            //looking back at this, i could have just used one interpolation, dont wanna touch it again
            string m3u8_link = title + $"/{path}" + $"/{title.Split("-")[1].Split(".")[0]}" + $"/{data_list[91]}" + $"/{data_list[90]}" + $"/{data_list[89]}" +
                               $".{data_list[88]}";
            StreamingLink.M3u8_link = m3u8_link;
            Streaming_links.Add(StreamingLink);
            // for (int i = 0; i <= data_list.Count-1; i++)
            // {
            //
            //          Debug.WriteLine(i);
            //          Debug.WriteLine(data_list[i]);
            // }
        }
        return JsonSerializer.Serialize(Streaming_links);
    }
}

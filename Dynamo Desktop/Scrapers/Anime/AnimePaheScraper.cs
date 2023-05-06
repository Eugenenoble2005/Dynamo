using DynamicData;
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
        /**Pray this doesn't fucking break randomely*/
        var AnimeDetails = new Dictionary<string, object>();
        string url = $"https://animepahe.com/anime/{Id}";
        string response = await _http.GetStringAsync(url);
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response);

        var anime_poster = htmlDoc.DocumentNode.SelectSingleNode("//a[@class='youtube-preview']");
        AnimeDetails["CoverImage"] = anime_poster.GetAttributeValue("href", null);

        AnimeDetails["description"] = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='anime-synopsis']")
            .InnerText;

        var anime_info = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class,'anime-info')]");
        var pElements = anime_info.SelectNodes("//p");
        AnimeDetails["JapaneseName"] = pElements[1].InnerText.Remove(0,9);
        AnimeDetails["Type"] = pElements[2].InnerText.Remove(0,6);
        AnimeDetails["EpisodeCount"] = pElements[3].InnerText.Remove(0,9);
        AnimeDetails["AnimeStatus"] = pElements[4].InnerText.Remove(0,7);
        AnimeDetails["Duration"] = pElements[5].InnerText.Remove(0,9);
        AnimeDetails["AirRange"] = pElements[6].InnerText.Remove(0,6);
        AnimeDetails["Season"] = pElements[7].InnerText.Remove(0,7);
        AnimeDetails["Studio"] = pElements[8].InnerText.Remove(0,7);
        AnimeDetails["Themes"] = pElements[9].InnerText.Remove(0,7);
        return JsonSerializer.Serialize(AnimeDetails);
    }
}

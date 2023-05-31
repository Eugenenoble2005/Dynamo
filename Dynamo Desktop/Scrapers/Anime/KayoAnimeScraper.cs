using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using HtmlAgilityPack;
using System.Diagnostics;
using Dynamo_Desktop.Models.Anime;

namespace Dynamo_Desktop.Scrapers.Anime;

public class KayoAnimeScraper
{
    private HttpClient _http = new HttpClient();
    public async Task<string> RecentEpisodes()
    {
        //what kinda anime site uses wordpress 💀
        string url = "http://kayoanime.com";
        string response = await _http.GetStringAsync(url);
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response);
        List<KayoAnimeRecentEpisodes> recent_episodes = new List<KayoAnimeRecentEpisodes>();
        HtmlNodeCollection grid_items = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class,'grid-item')]");
        foreach (var grid_item in grid_items)
        {
            KayoAnimeRecentEpisodes episode = new KayoAnimeRecentEpisodes();
            episode.Title = grid_item.SelectSingleNode(".//h2[@class='thumb-title']").InnerText;
            episode.Image = grid_item.GetAttributeValue("style", null).TrimStart("background-image: url(".ToCharArray()).TrimEnd(")".ToCharArray());
            episode.AnimeId = grid_item.SelectSingleNode(".//h2[@class='thumb-title']").SelectSingleNode("//a").GetAttributeValue("href", null).TrimStart("https://kayoanime.com/".ToCharArray()).TrimEnd("/".ToCharArray());
            recent_episodes.Add(episode);
        }
        return JsonSerializer.Serialize(recent_episodes);
       
    }
}

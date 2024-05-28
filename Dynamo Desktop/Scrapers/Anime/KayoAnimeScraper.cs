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
using System.Net.Http.Json;

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
    public async Task<string> Search(string Query)
    {

        string url = "https://kayoanime.com/wp-admin/admin-ajax.php";
      
        var response = await _http.PostAsync(url,new StringContent($"action=tie_ajax_search&query={Query}",Encoding.UTF8, "application/x-www-form-urlencoded"));
        string json = await response.Content.ReadAsStringAsync();
        var search_response = JsonSerializer.Deserialize<SearchResponseType>(json);
        List<KayoAnimeSearch> search_results = new List<KayoAnimeSearch>();
        foreach(Suggestion suggestion in search_response.suggestions)
        {
            //43
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(suggestion.layout);
            Debug.WriteLine(suggestion.layout);
            KayoAnimeSearch search_result = new KayoAnimeSearch();
            HtmlNode ImageNode = htmlDoc.DocumentNode.SelectSingleNode("//img");
            if(ImageNode != null)
            {
                search_result.Image = ImageNode.GetAttributeValue("src", null);
            }
            search_result.AnimeId = suggestion.url.TrimStart("https://kayoanime.com/".ToCharArray()).TrimEnd("/".ToCharArray()); ;
            search_result.Title = suggestion.value;
            search_results.Add(search_result);
        }
        return JsonSerializer.Serialize(search_results);
    }
}
 class SearchResponseType
{
    public string query { get; set; }
    public List<Suggestion> suggestions { get; set; }
}

 class Suggestion
{
    public string layout { get; set; }
    public string value { get; set; }
    public string url { get; set; }
}

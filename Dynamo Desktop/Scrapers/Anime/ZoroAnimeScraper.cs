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

public class ZoroAnimeScraper
{
	private HttpClient _http = new HttpClient();
	public async Task<string> RecentEpisodes(int Page = 1, string sort = "recent")
	{

		string url =  sort == "recent" ?  $"https://zoro.to/recently-updated?page={Page}" : $"https://zoro.to/top-airing?page={Page}";

		string html_dom = await _http.GetStringAsync(url);
		HtmlDocument htmlDoc = new HtmlDocument();
		htmlDoc.LoadHtml(html_dom);
		List<ZoroAnimeRecentEpisodes> zoro_recent_episodes = new List<ZoroAnimeRecentEpisodes>();
		HtmlNode main_content = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='main-content']");
		HtmlNodeCollection flw_items = main_content.SelectNodes(".//div[contains(@class,'flw-item')]");
		foreach(HtmlNode flw_item in flw_items)
		{
			ZoroAnimeRecentEpisodes Episode = new ZoroAnimeRecentEpisodes();
			Episode.Title = flw_item.SelectSingleNode(".//a[@class='dynamic-name']").InnerText;
			Episode.Poster = flw_item.SelectSingleNode(".//img").GetAttributeValue("data-src", null);
			Episode.EpisodeId = flw_item.SelectSingleNode(".//a[@class='dynamic-name']").GetAttributeValue("href",null).TrimStart('/');
			zoro_recent_episodes.Add(Episode);
		}
		return JsonSerializer.Serialize(zoro_recent_episodes);
	}
}

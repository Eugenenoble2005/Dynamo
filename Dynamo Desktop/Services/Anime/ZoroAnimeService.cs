using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.Scrapers.Anime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Services.Anime
{
	public class ZoroAnimeService
	{
		private ZoroAnimeScraper _zoroScraper = new ZoroAnimeScraper();
		private static HttpClient client = new HttpClient();
		public async Task<List<ZoroAnimeRecentEpisodes>> RecentEpisodes(int Page=1)
		{
			try
			{
				string json = await _zoroScraper.RecentEpisodes(Page);
				return JsonSerializer.Deserialize<List<ZoroAnimeRecentEpisodes>>(json);
			}
			catch {
				return default;
			}
		}
		public async Task<List<ZoroAnimeRecentEpisodes>> PopularAnime(int Page = 1)
		{
			try
			{
				string json = await _zoroScraper.RecentEpisodes(Page,sort:"popular");
				return JsonSerializer.Deserialize<List<ZoroAnimeRecentEpisodes>>(json);
			}
			catch
			{
				return default;
			}
		}
		public async Task<ZoroAnimeSearch> Search(string Query="", int Page = 1)
		{
			string search_endpoint = $"https://api.consumet.org/anime/zoro/{Query}?page={Page}";
			try
			{
				HttpResponseMessage response = await client.GetAsync(search_endpoint);
				if (response.IsSuccessStatusCode)
				{
					string json = await response.Content.ReadAsStringAsync();
					return JsonSerializer.Deserialize<ZoroAnimeSearch>(json);
				}
				return default;
			}
			catch
			{
				return default;
			}
		}
		public async Task<ZoroAnimeInfo> Info(string Id)
		{
			string url = $"https://api.consumet.org/anime/zoro/info?id={Id}";
			try
			{
				HttpResponseMessage response = await client.GetAsync(url);
				if (response.IsSuccessStatusCode)
				{
					string json = await response.Content.ReadAsStringAsync();
					return JsonSerializer.Deserialize<ZoroAnimeInfo>(json);
				}
				return default;
			}
			catch
			{
				return default;
			}
		}
		public async Task<ZoroAnimeStreamingLinks> StreamingLinks(string EpisodeId)
		{
			string streaming_links = $"https://api.consumet.org/anime/zoro/watch?episodeId={EpisodeId}";
		
			try
			{
				HttpResponseMessage response = await client.GetAsync(streaming_links);
				if (response.IsSuccessStatusCode)
				{
					string json = await response.Content.ReadAsStringAsync();
					
					return JsonSerializer.Deserialize<ZoroAnimeStreamingLinks>(json);
				}
				return default;
			}
			catch
			{
				return default;
			}
		}
	}
}

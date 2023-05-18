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
    public class AnimePaheService
    {
        private static HttpClient client = new HttpClient();
        private static AnimePaheScraper _animePaheScraper = new AnimePaheScraper();

        public async Task<List<AnimePaheStreamingLinks>> StreamingLinks(string AnimeId, string EpisodeId)
        {
            try
            {
                string json = await _animePaheScraper.EpisodeStreamLinks(AnimeId: AnimeId, EpisodeId: EpisodeId);
                return JsonSerializer.Deserialize<List<AnimePaheStreamingLinks>>(json);
            }
            catch
            {
                return default;
            }
        }
        public async Task<List<PaheResult>> AllEpisodes(string Id = "",int Page=1)
        {
            List<PaheResult> Episode_List = new List<PaheResult>();
            async Task GetEpisodes(int Page)
            {
                string endpoint = $"https://animepahe.com/api?m=release&id={Id}&sort=episode_asc&page={Page}&per_page=2";
                try
                {
                    HttpResponseMessage response = await client.GetAsync(endpoint);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        AnimePaheRecentEpisodes Episodes = JsonSerializer.Deserialize<AnimePaheRecentEpisodes>(json);
                        foreach(var episode in Episodes.data)
                        {
                            Episode_List.Add(episode);
                        }
                        //use recursion to obtain all episodes.
                        if(Episodes.next_page_url != null)
                        {
                            await GetEpisodes(Page + 1);
                        }
                    }
                }
                catch {
                    return;
                }
            }
             await GetEpisodes(Page);
            return Episode_List;
        }

        public  async Task<AnimePaheAnimeInfo> Info(string Id = "")
        {
            try
            {
                string json = await _animePaheScraper.AnimeInfo(Id: Id);
                return JsonSerializer.Deserialize<AnimePaheAnimeInfo>(json);
            }
            catch
            {
                return default;
            }
        }

        public Task<T?> PopularEpisodes<T>(int Page)
        {
            throw new NotImplementedException();
        }

        public async Task<AnimePaheRecentEpisodes> RecentEpisodes(int Page=1, int Type=1)
        {
            string recent_releases_endpoint = $"https://animepahe.com/api?m=airing&page={Page}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(recent_releases_endpoint);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<AnimePaheRecentEpisodes>(json);
                }
                return default;
            }
            catch
            {
                return default;
            }
        }

        public async Task<AnimePaheSearch> Search(string Query, int Page)
        {
            string endpoint = $"https://animepahe.com/api?m=search&q={Query}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<AnimePaheSearch>(json);
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

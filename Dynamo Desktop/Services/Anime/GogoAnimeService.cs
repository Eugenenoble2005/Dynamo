using Dynamo_Desktop.Models.Anime;
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
    public class GogoAnimeService
    {
        private HttpClient client = new HttpClient();
        public async Task<GogoAnimeRecentEpisodes> RecentEpisodes()
        {
            string recent_releases_endpoint = "https://api.consumet.org/anime/gogoanime/recent-episodes";
            try
            {
                HttpResponseMessage response = await client.GetAsync(recent_releases_endpoint);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<GogoAnimeRecentEpisodes>(json);
                }
                return null;
            }
            catch {
                return null;
            }
        }
    }
}

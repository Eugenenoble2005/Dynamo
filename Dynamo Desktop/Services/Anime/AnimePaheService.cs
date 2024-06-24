using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.Scrapers.Anime;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace Dynamo_Desktop.Services.Anime
{
    public class AnimePaheService : IAnimeService
    {
        public Task<List<PopularAnime>> PopularAnime(int Page = 1)
        {
            try
            {
                return RecentAnime(Page: Page);
            }
            catch
            {
                return default;
            }
        }

        public async Task<List<PopularAnime>> RecentAnime(int Page = 1)
        {
            try
            {
                return JsonSerializer.Deserialize<List<PopularAnime>>(
                    await new AnimePaheScraper().RecentAnime(Page: Page));
            }
            catch (Exception e)
            {
                return default;
            }
        }

        public async Task<List<PopularAnime>> Search(string Query, int Page = 1)
        {
            try
            {
                return JsonSerializer.Deserialize<List<PopularAnime>>(
                    await new AnimePaheScraper().Search(Query: Query));
            }
            catch
            {
                return default;
            }
        }

        public async Task<AnimeInfo> Info(string Query)
        {
            try
            {
                return JsonSerializer.Deserialize<AnimeInfo>(
                    await new AnimePaheScraper().AnimeInfo(Query: Query));
            }
            catch
            {
                return default;
            }
        }

        public async Task<List<AnimeStreamingLinks>> StreamingLinks(string Query, int Episode = 1)
        {
            try
            {
                return JsonSerializer.Deserialize<List<AnimeStreamingLinks>>(
                    await new AnimePaheScraper().EpisodeStreamLinks(AnimeId: Query, Episode: Episode));
            }
            catch
            {
                return default;
            }
        }
    }
}

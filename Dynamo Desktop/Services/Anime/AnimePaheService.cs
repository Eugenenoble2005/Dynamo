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
            return RecentAnime(Page: Page);
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

        public Task<List<PopularAnime>> Search(string Query, int Page = 1)
        {
            throw new NotImplementedException();
        }

        public Task<AnimeInfo> Info(string Query)
        {
            throw new NotImplementedException();
        }

        public Task<List<AnimeStreamingLinks>> StreamingLinks(string Query, int Episode = 1)
        {
            throw new NotImplementedException();
        }
    }
}

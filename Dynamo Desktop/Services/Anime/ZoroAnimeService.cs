using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.Scrapers.Anime;

namespace Dynamo_Desktop.Services.Anime;

public class ZoroAnimeService : IAnimeService
{
    public async Task<List<PopularAnime>> PopularAnime(int Page = 1)
    {
        try
        {
            return JsonSerializer.Deserialize<List<PopularAnime>>(await
                new ZoroAnimeScraper().PopularOrRecent(Page: Page, Query: "Popular"));
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
            return JsonSerializer.Deserialize<List<PopularAnime>>(await
                new ZoroAnimeScraper().PopularOrRecent(Page: Page, Query: "Recent"));
        }
        catch
        {
            return default;
        }
    }

    public async Task<List<PopularAnime>> Search(string Query, int Page = 1)
    {
        throw new System.NotImplementedException();
    }

    public async Task<AnimeInfo> Info(string Query)
    {
        throw new System.NotImplementedException();
    }

    public async Task<List<AnimeStreamingLinks>> StreamingLinks(string Query, int Episode = 1)
    {
        throw new System.NotImplementedException();
    }
}
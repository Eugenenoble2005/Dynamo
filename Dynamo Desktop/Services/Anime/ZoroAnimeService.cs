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
                new ZoroAnimeScraper().PopularOrRecentOrSearch(Page: Page, Query: "Popular"));
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
                new ZoroAnimeScraper().PopularOrRecentOrSearch(Page: Page, Query: "Recent"));
        }
        catch
        {
            return default;
        }
    }

    public async Task<List<PopularAnime>> Search(string Query, int Page = 1)
    {
        try
        {
            return JsonSerializer.Deserialize<List<PopularAnime>>(await
                new ZoroAnimeScraper().PopularOrRecentOrSearch(Page: Page, Query: "Search",SearchQuery:Query));
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
            return JsonSerializer.Deserialize<AnimeInfo>(await
                new ZoroAnimeScraper().Info( Query: Query));
        }
        catch
        {
            return default;
        }
    }

    public async Task<List<AnimeStreamingLinks>> StreamingLinks(string Query, int Episode = 1)
    {
        return null;
    }
}
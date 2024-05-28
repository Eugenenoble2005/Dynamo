using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.Scrapers.Anime;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Services.Anime;

public class GogoAnimeService : IAnimeService
{
    public async Task<List<PopularAnime>> PopularAnime(int Page = 1)
    {
        return JsonSerializer.Deserialize<List<PopularAnime>>(await new GogoAnimeScraper().PopularOrRecent(Query:"Popular",Page:Page));
    }

    public async Task<List<PopularAnime>> RecentAnime(int Page = 1)
    {
        return JsonSerializer.Deserialize<List<PopularAnime>>(await new GogoAnimeScraper().PopularOrRecent(Query: "Recent",Page:Page));
    }

    public async Task<List<PopularAnime>> Search(string Query, int Page = 1)
    {
        return JsonSerializer.Deserialize<List<PopularAnime>>(await new GogoAnimeScraper().Search(Query: Query,Page:Page));
    }
    public async Task<AnimeInfo> Info(string Query)
    {
        return JsonSerializer.Deserialize<AnimeInfo>(await new GogoAnimeScraper().Info(Query: Query));
    }

}

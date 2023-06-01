using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.Scrapers.Anime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Services.Anime;

public class KayoAnimeService
{
    private KayoAnimeScraper _kayoScraper = new KayoAnimeScraper();
    public async Task<List<KayoAnimeRecentEpisodes>> RecentEpisodes()
    {
        try
        {
            string json = await _kayoScraper.RecentEpisodes();
            return JsonSerializer.Deserialize<List<KayoAnimeRecentEpisodes>>(json);
        }
        catch
        {
            return default;
        }
    }
    public async Task<List<KayoAnimeSearch>> Search(string Query)
    {
        try
        {
            string json = await _kayoScraper.Search(Query);
            return JsonSerializer.Deserialize<List<KayoAnimeSearch>>(json);
        }
        catch
        {
            return default;
        }
    }
}

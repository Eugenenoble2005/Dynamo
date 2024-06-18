using System;
using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.Scrapers.Anime;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Dynamo_Desktop.Services.Anime;

public class GogoAnimeService : IAnimeService
{
    public async Task<List<PopularAnime>> PopularAnime(int Page = 1)
    {
        try
        {
            return JsonSerializer.Deserialize<List<PopularAnime>>(
                await new GogoAnimeScraper().PopularOrRecent(Query: "Popular", Page: Page));
        }
        catch (Exception e)
        {
            return default;
        }

    }

    public async Task<List<PopularAnime>> RecentAnime(int Page = 1)
    {
        try
        {
            return JsonSerializer.Deserialize<List<PopularAnime>>(
                await new GogoAnimeScraper().PopularOrRecent(Query: "Recent", Page: Page));
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
                await new GogoAnimeScraper().Search(Query: Query, Page: Page));
        }
        catch (Exception e)
        {
            return default;
        }

    }

    public async Task<AnimeInfo> Info(string Query)
    {
        /*
         * API experiences SSL errors. If that occurs. Run method again
         */
        int attempts = 1;
        try
        {
            var result = JsonSerializer.Deserialize<AnimeInfo>(await new GogoAnimeScraper().Info(Query: Query));
            if (result == null || result == default)
            {
                return result;
            }
            else
            {
                return result;
            }
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
            //provider is wonky and may fail sometimes. Continue retrying process until it succeeds.
            int attempts = 1;
            while (attempts <= 10)
            {
                var result = JsonSerializer.Deserialize<List<AnimeStreamingLinks>>(
                    await new GogoAnimeScraper().StreamingLinks(Query: Query, Episode: Episode));
                if (result == null || result.Count == 0)
                {
                    attempts++;
                }
                else
                {
                    return result;
                }
            }
            //should never get to this.
            return default;

        }
        catch
        {
            return default;
        }
    }
}



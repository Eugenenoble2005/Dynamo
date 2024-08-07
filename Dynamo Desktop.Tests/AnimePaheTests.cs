using System.Diagnostics;
using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.Scrapers.Anime;
using Dynamo_Desktop.Services.Anime;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Dynamo_Desktop.Tests;

[TestClass]
public class AnimePaheTests
{
    private AnimePaheScraper _scraper = new();
    [TestMethod]
    public async Task TestRecentAndPopular()
    {
        //check top 10  results
        for (int i = 1; i <= 10; i++)
        {
            var response =  JsonSerializer.Deserialize<List<PopularAnime>>(
                await _scraper.RecentAnime(Page: i));
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<List<PopularAnime>>(response);
        }
    }
    [TestMethod]
    public async Task TestSearch()
    {
        foreach (var searchTerm in Commons.SearchTerms)
        {
            var response = JsonSerializer.Deserialize<List<PopularAnime>>(
                await _scraper.Search(Query: searchTerm));
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<List<PopularAnime>>(response);
        }
    }
    [TestMethod]
    public async Task TestInfo()
    {
        var recents =  JsonSerializer.Deserialize<List<PopularAnime>>(
            await _scraper.RecentAnime(Page: 1));
        foreach (var popularAnime in recents)
        {
            string AnimeId = popularAnime.AnimeId;
            var info = JsonSerializer.Deserialize<AnimeInfo>(
                await _scraper.AnimeInfo(Query: AnimeId));
            Assert.IsNotNull(info);
            Assert.IsInstanceOfType<AnimeInfo>(info);
        }
    }
    
    [TestMethod]
    public async Task TestStreamingLinks()
    {
        var recents =  JsonSerializer.Deserialize<List<PopularAnime>>(
            await _scraper.RecentAnime(Page: 1));
        foreach (var popularAnime in recents)
        {
            string AnimeId = popularAnime.AnimeId;
            var info = JsonSerializer.Deserialize<AnimeInfo>(
                await _scraper.AnimeInfo(Query: AnimeId));
            int key = 0;
            foreach (var episode in info.Episodes)
            {
                //cap testing at 10 per episode. If something will break, it will likely break here 
                key++;
                var streamLinks = JsonSerializer.Deserialize<List<AnimeStreamingLinks>>(
                    await _scraper.EpisodeStreamLinks(AnimeId: AnimeId, Episode: episode.EpisodeNumber));
                Assert.IsNotNull(streamLinks);
                Assert.IsInstanceOfType<List<AnimeStreamingLinks>>(streamLinks);
                if (key == 5)
                {
                    break;
                }
            }
        }
    }
}

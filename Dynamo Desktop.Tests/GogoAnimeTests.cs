using System.Diagnostics;
using System.Text.Json;
using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.Scrapers.Anime;

namespace Dynamo_Desktop.Tests;

//i should have abstracted this but i was just too lazy. Write Everything twice.
[TestClass]
public class GogoAnimeTests
{
    private GogoAnimeScraper _scraper = new();
    [TestMethod]
    public async Task TestRecent()
    {
        //check top 10  results
        for (int i = 1; i <= 10; i++)
        {
            var response =  JsonSerializer.Deserialize<List<PopularAnime>>(
                await _scraper.PopularOrRecent(Page: i,"Recent"));
            // if (response?.Count > 1)
            // {
            //     Debug.WriteLine(i);
            // }
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<List<PopularAnime>>(response);
        }
    }

    [TestMethod]
    public async Task TestPopular()
    {
        for (int i = 1; i <= 10; i++)
        {
            var response =  JsonSerializer.Deserialize<List<PopularAnime>>(
                await _scraper.PopularOrRecent(Page: i,"Popular"));
            // if (response?.Count > 1)
            // {
            //     Debug.WriteLine(i);
            // }
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
            await _scraper.PopularOrRecent(Page: 1));
        foreach (var popularAnime in recents)
        {
            string AnimeId = popularAnime.AnimeId;
            var info = JsonSerializer.Deserialize<AnimeInfo>(
                await _scraper.Info(Query: AnimeId));
            Assert.IsNotNull(info);
            Assert.IsInstanceOfType<AnimeInfo>(info);
        }
    }
    [TestMethod]
    public async Task TestStreamingLinks()
    {
        var recents =  JsonSerializer.Deserialize<List<PopularAnime>>(
            await _scraper.PopularOrRecent(Page: 1));
        foreach (var popularAnime in recents)
        {
            string AnimeId = popularAnime.AnimeId;
            var info = JsonSerializer.Deserialize<AnimeInfo>(
                await _scraper.Info(Query: AnimeId));
            int key = 0;
            foreach (var episode in info.Episodes)
            {
                //cap testing at 10 per episode. If something will break, it will likely break here 
                key++;
                var streamLinks = JsonSerializer.Deserialize<List<AnimeStreamingLinks>>(
                    await _scraper.StreamingLinks(Query: AnimeId, Episode: episode.EpisodeNumber));
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
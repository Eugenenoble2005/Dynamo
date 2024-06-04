using Dynamo_Desktop.Models.Anime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Services.Anime;

public interface IAnimeService
{
    public  Task<List<PopularAnime>> PopularAnime(int Page = 1);
    public Task<List<PopularAnime>> RecentAnime(int Page=1);
    public Task<List<PopularAnime>> Search(string Query, int Page=1);
    public Task<AnimeInfo> Info(string Query);
    public Task<List<AnimeStreamingLinks>> StreamingLinks(string Query, int Episode = 1);

}

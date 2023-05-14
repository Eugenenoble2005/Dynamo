using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Services.Anime;

public interface IAnimeService
{
    /*Get most recently released episode from provider*/
    Task<T?> RecentEpisodes<T>(int Page = 1, int Type = 1);
    /*Get popular or top airing episodes from provider*/
    Task<T?> PopularEpisodes<T>(int Page = 1);
    /*Search for particular anime */
    Task<T?> Search<T>(string Query, int Page = 1);
    /*Get info on provider*/
    Task<T?> Info<T>(string Id = "");

    /*Get all episodes for a particular Anime*/
    Task<T> AllEpisodes<T>(string Id = "",int Page=1);
}

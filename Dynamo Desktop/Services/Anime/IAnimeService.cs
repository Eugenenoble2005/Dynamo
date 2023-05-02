using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Services.Anime;

public interface IAnimeService
{
    /*Get most recently released episode from provider*/
    Task<T?> RecentEpisodes<T>(int Page = 1,int Type = 1);
    Task<T?> PopularEpisodes<T>(int Page=1);
    Task<T?> Search<T>(string Query, int Page=1);
}

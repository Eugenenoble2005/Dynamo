using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Services.Anime
{
    public abstract class AnimeService<T>
    {
        /*Get most recently released episode from provider*/
        public abstract Task<T?> RecentEpisodes(int Page = 1, int Type = 1);
        /*Get popular or top airing episodes from provider*/
        public abstract Task<T?> PopularEpisodes(int Page = 1);
        /*Search for particular anime */
        public abstract Task<T?> Search(string Query, int Page = 1);
        /*Get info on provider*/
        public abstract Task<T> Info(string Id = "");

        /*Get all episodes for a particular Anime*/
        public abstract Task<T> AllEpisodes(string Id = "", int Page = 1);
    }
}

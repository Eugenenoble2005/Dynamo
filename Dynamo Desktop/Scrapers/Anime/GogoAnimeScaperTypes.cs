using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Scrapers.Anime
{
    internal partial class GogoAnimeScraper
    {
        public class GogoAnimePopularJsonRequest
        {
            public bool status { get; set; }
            public GogoAnimePopularJsonRequestData data { get; set; }
        }

        public class GogoAnimePopularJsonRequestData
        {
            public GogoAnimePopularJsonRequestAnimedata[] animeData { get; set; }
            public int entryCount { get; set; }
            public string[] pagination { get; set; }
        }

        public class GogoAnimePopularJsonRequestAnimedata
        {
            public string name { get; set; }
            public string img { get; set; }
            public string url { get; set; }
            public string time { get; set; }
        }

    }
}

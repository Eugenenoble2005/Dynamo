using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Models.Anime
{
    public class GogoPopularResult
    {
        public string id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string url { get; set; }
        public List<string> genres { get; set; }
    }

    public class GogoAnimePopularAnime
    {
        public dynamic currentPage { get; set; }
        public bool hasNextPage { get; set; }
        public List<GogoPopularResult> results { get; set; }
    }


}

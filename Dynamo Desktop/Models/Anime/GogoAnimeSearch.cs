using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Models.Anime
{
 
    public class GogoSearchResult
    {
        public string id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string releaseDate { get; set; }
        public string subOrDub { get; set; }
    }

    public class GogoAnimeSearch
    {
        public dynamic currentPage { get; set; }
        public bool hasNextPage { get; set; }
        public List<GogoSearchResult> results { get; set; }
    }


}

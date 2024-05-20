using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Scrapers.Anime;

internal partial class GogoAnimeScraper
{
    public class GogoAnimePopularJsonResponse
    {
        public bool status { get; set; }
        public GogoAnimePopularJsonResponseData data { get; set; }
    }

    public class GogoAnimePopularJsonResponseData
    {
        public GogoAnimePopularJsonResponseAnimedata[] animeData { get; set; }
        public int entryCount { get; set; }
        public string[] pagination { get; set; }
    }

    public class GogoAnimePopularJsonResponseAnimedata
    {
        public string name { get; set; }
        public string img { get; set; }
        public string url { get; set; }
        public string time { get; set; }
    }
    //search

    public class GogoAnimeSearchJsonResponse
    {
        public bool status { get; set; }
        public GogoAnimeSearchJsonResponseData data { get; set; }
    }

    public class GogoAnimeSearchJsonResponseData
    {
        public GogoAnimeSearchJsonResponseAnimedata[] animeData { get; set; }
        public object[] pagination { get; set; }
    }

    public class GogoAnimeSearchJsonResponseAnimedata
    {
        public string episodeURL { get; set; }
        public string episodeName { get; set; }
        public string episodeImage { get; set; }
        public string episodeTime { get; set; }
        public string animeName { get; set; }
    }

}

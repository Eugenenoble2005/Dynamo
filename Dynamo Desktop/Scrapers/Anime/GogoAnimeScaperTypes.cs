using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Scrapers.Anime;

public partial class GogoAnimeScraper
{
    private class GogoAnimePopularJsonResponse
    {
        public bool status { get; set; }
        public GogoAnimePopularJsonResponseData data { get; set; }
    }

    private class GogoAnimePopularJsonResponseData
    {
        public GogoAnimePopularJsonResponseAnimedata[] animeData { get; set; }
        public int entryCount { get; set; }
        public string[] pagination { get; set; }
    }

    private class GogoAnimePopularJsonResponseAnimedata
    {
        public string name { get; set; }
        public string img { get; set; }
        public string url { get; set; }
        public string time { get; set; }
    }
    //search

    private class GogoAnimeSearchJsonResponse
    {
        public bool status { get; set; }
        public GogoAnimeSearchJsonResponseData data { get; set; }
    }

    private class GogoAnimeSearchJsonResponseData
    {
        public GogoAnimeSearchJsonResponseAnimedata[] animeData { get; set; }
        public object[] pagination { get; set; }
    }

    private class GogoAnimeSearchJsonResponseAnimedata
    {
        public string episodeURL { get; set; }
        public string episodeName { get; set; }
        public string episodeImage { get; set; }
        public string episodeTime { get; set; }
        public string animeName { get; set; }
    }
    //info

    private class GogoAnimeInfoJsonResponse
    {
        public bool status { get; set; }
        public GogoAnimeInfoJsonResponseData data { get; set; }
    }

    private class GogoAnimeInfoJsonResponseData
    {
        public GogoAnimeInfoJsonResponseAnimedata animeData { get; set; }
    }

    private class GogoAnimeInfoJsonResponseAnimedata
    {
        public string[] episodeURL { get; set; }
        public int[] episodeNumber { get; set; }
        public string episodeName { get; set; }
        public string videoDetailName { get; set; }
        public string videoSummary { get; set; }
        public string videoIframe { get; set; }
        public string downloadUrl { get; set; }
    }
        private class GogoAnimeStreamingLinksJsonResponse
    {
        public bool status { get; set; }
        public GogoAnimeStreamingLinksJsonResponseData data { get; set; }
    }

    private class GogoAnimeStreamingLinksJsonResponseData
    {
        public GogoAnimeStreamingLinksJsonResponseSources[] sources { get; set; }
    }

    private class GogoAnimeStreamingLinksJsonResponseSources
    {
        public string url { get; set; }
        public bool isM3U8 { get; set; }
        public string quality { get; set; }
    }


}

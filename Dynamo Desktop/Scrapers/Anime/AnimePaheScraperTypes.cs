namespace Dynamo_Desktop.Scrapers.Anime;

public partial class AnimePaheScraper
{
            private class AnimePaheRecentEpisodesJsonResponse
        {
            public int total { get; set; }
            public int per_page { get; set; }
            public int current_page { get; set; }
            public int last_page { get; set; }
            public string next_page_url { get; set; }
            public object prev_page_url { get; set; }
            public int from { get; set; }
            public int to { get; set; }
            public AnimePaheRecentEpisodesJsonResponseData[] data { get; set; }
        }

        private class AnimePaheRecentEpisodesJsonResponseData
        {
            public int id { get; set; }
            public int anime_id { get; set; }
            public string anime_title { get; set; }
            public string anime_session { get; set; }
            public int episode { get; set; }
            public int episode2 { get; set; }
            public string edition { get; set; }
            public string fansub { get; set; }
            public string snapshot { get; set; }
            public string disc { get; set; }
            public string session { get; set; }
            public int filler { get; set; }
            public string created_at { get; set; }
            public int completed { get; set; }
        }

        //search
            private class AnimePaheSearchJsonResponse
    {
        public int total { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public int last_page { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public AnimePaheSearchJsonResponseData[] data { get; set; }
    }

    public class AnimePaheSearchJsonResponseData
    {
        public int id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public int episodes { get; set; }
        public string status { get; set; }
        public string season { get; set; }
        public int year { get; set; }
        public double score { get; set; }
        public string poster { get; set; }
        public string session { get; set; }
    }

    private class AnimePaheEpisodesJsonResponse
{
    public int total { get; set; }
    public int per_page { get; set; }
    public int current_page { get; set; }
    public int last_page { get; set; }
    public string next_page_url { get; set; }
    public object prev_page_url { get; set; }
    public int from { get; set; }
    public int to { get; set; }
    public AnimePaheEpisodesJsonResponseData[] data { get; set; }
}

public class AnimePaheEpisodesJsonResponseData
{
    public int id { get; set; }
    public int anime_id { get; set; }
    public int episode { get; set; }
    public int episode2 { get; set; }
    public string edition { get; set; }
    public string title { get; set; }
    public string snapshot { get; set; }
    public string disc { get; set; }
    public string audio { get; set; }
    public string duration { get; set; }
    public string session { get; set; }
    public int filler { get; set; }
    public string created_at { get; set; }
}


        

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Models.Anime;


public class GogoStreamingLinksHeaders
{
    public string Referer { get; set; }
    public string watchsb { get; set; }
    public string UserAgent { get; set; }
}

public class GogoAnimeStreamingLinks
{
    public GogoStreamingLinksHeaders headers { get; set; }
    public List<GogoStreamingLinksSource> sources { get; set; }
}

public class GogoStreamingLinksSource
{
    public string url { get; set; }
    public string quality { get; set; }
    public bool isM3U8 { get; set; }
}

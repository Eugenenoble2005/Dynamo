using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Models.Anime;


public class GogoAnimeEpisodes
{
    public string? id { get; set; }
    public int? number { get; set; }
    public string? url { get; set; }
}

public class GogoAnimeInfo
{
    public string? id { get; set; }
    public string title { get; set; }
    public string? url { get; set; }
    public string? image { get; set; }
    public string? releaseDate { get; set; }
    public string? description { get; set; }
    public List<string>? genres { get; set; }
    public string? subOrDub { get; set; }
    public string? type { get; set; }
    public string? status { get; set; }
    public string? otherName { get; set; }
    public int? totalEpisodes { get; set; }
    public List<GogoAnimeEpisodes>? episodes { get; set; }
}



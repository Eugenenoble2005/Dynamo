using System.Collections.Generic;
using Dynamo_Desktop.ViewModels.Anime;

namespace Dynamo_Desktop.Models.Anime;
/**
 * Model will be used for both popular and recent anime. Will be collected by scrapers in a List like so List<PopularAnime>
 */
public class PopularAnime
{
    public string AnimeId { get; set; }
    public string Title { get; set; }

    //default to episode 1
    public int Episode { get; set; } = 1;
    
    public string Status { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
}
public class AnimeInfo
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int EpisodeCount { get; set; }
    public string Image { get; set; }
    
    public List<AnimeEpisodes> Episodes { get; set; }
}

public class AnimeStreamingLinks
{
    public string Quality { get; set; }
    public string Source { get; set; }
}
public class AnimeIndexToDetailsRouteParams
{
    public AnimeProviders? Provider { get; set; }
    public string? AnimeId { get; set; }
    public int EpisodeNumber { get; set; } = 1;
}

public class AnimeEpisodes
{
    public int EpisodeNumber { get; set; }
    public string EpisodeId { get; set; }
}
public enum AnimeProviders
{
    GogoAnime,
    AnimePahe,
    ZoroAnime
}
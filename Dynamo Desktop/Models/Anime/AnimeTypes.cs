using Dynamo_Desktop.ViewModels.Anime;

namespace Dynamo_Desktop.Models.Anime;
/**
 * Model will be used for both popular and recent anime.
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
}
public class AnimeIndexToDetailsRouteParams
{
    public AnimeProviders? Provider { get; set; }
    public string? AnimeId { get; set; }
    public string? EpisodeNumber { get; set; }
}
public enum AnimeProviders
{
    GogoAnime,
    AnimePahe,
    ZoroAnime
}
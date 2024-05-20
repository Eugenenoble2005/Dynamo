namespace Dynamo_Desktop.Models.Anime;
/**
 * Model will be used for both popular and recent anime.
 */
public class PopularAnime
{
    public string AnimeId { get; set; }
    public string Title { get; set; }
    public int Episode { get; set; }
    public string status { get; set; }
    public string description { get; set; }
    public string Image { get; set; }
}

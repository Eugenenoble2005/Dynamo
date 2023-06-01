using Dynamo_Desktop.Services.Anime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Models.Anime;

public class PaheResult
{
    private AnimePaheService _animePaheService = new AnimePaheService();
    public int id { get; set; }
    public int anime_id { get; set; }
    public string anime_title { get; set; }
    public string anime_session { get; set; }
    //i havent a fucking clue why but sometimes animepahe sends this as a float
    public dynamic episode { get; set; }
    public dynamic episode2 { get; set; }
    public string edition { get; set; }
    public string fansub { get; set; }
    public string snapshot { get; set; }
    public string disc { get; set; }
    public string session { get; set; }
    public int filler { get; set; }
    public string created_at { get; set; }
    public int completed { get; set; }
    public bool IsCurrentEpisode { get; set; }

}

public class AnimePaheRecentEpisodes
{
    public int total { get; set; }
    public int per_page { get; set; }
    public int current_page { get; set; }
    public int last_page { get; set; }
    public string next_page_url { get; set; }
    public string prev_page_url { get; set; }
    public int from { get; set; }
    public int to { get; set; }
    public List<PaheResult> data { get; set; }
}

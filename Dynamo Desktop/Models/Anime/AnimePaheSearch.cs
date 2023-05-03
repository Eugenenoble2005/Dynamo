using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dynamo_Desktop.Models.Anime
{

    public class PaheSearchResult
    {
        public int? id { get; set; }
        public string? title { get; set; }
        public string? type { get; set; }
        public int? episodes { get; set; }
        public string? status { get; set; }
        public string? season { get; set; }
        public int? year { get; set; }
        public double? score { get; set; }
        public string? poster { get; set; }
        public string? session { get; set; }
    }

    public class AnimePaheSearch
    {
        public int? total { get; set; }
        public int? per_page { get; set; }
        public int? current_page { get; set; }
        public int? last_page { get; set; }
        public int? from { get; set; }
        public int? to { get; set; }

        public int? ResultLength => data?.Count ?? 0;
        public List<PaheSearchResult> data { get; set; }
    }


}

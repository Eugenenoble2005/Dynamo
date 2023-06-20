using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Models.Hentai
{
    //using this for search results to.
   public class RecentHentai
    {
        public string HentaiId { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
    }

   public class HentaiDetails
   {
       public string Title { get; set; }
       public string Description { get; set; }
       public int Views { get; set; }
       public string poster { get; set; }
       public List<string> Tags { get; set; }
       public List<HentaiStreamLinks> StreamLinks { get; set; }
   }

   public class HentaiStreamLinks
   {
       public string Quality { get; set; }
       public string Link { get; set; }
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Models
{
  
    public class Settings
    {
        public Providers Providers { get; set; }
    }

    public class Providers
    {
        public Provider gogoanime { get; set; }
        public Provider  animepahe { get; set; }
    }

    public class Provider
    {
        public string host { get; set; }
    }

}

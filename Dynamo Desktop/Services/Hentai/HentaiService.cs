using Dynamo_Desktop.Models.Hentai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Services.Hentai
{
    public abstract class HentaiService
    {
        public abstract Task<List<RecentHentai>> Recent();
        public  abstract Task Popular();
        public  abstract Task<List<RecentHentai>> Search(string Query="");

        public abstract Task<HentaiDetails> Info(string Query = "");

    }
}

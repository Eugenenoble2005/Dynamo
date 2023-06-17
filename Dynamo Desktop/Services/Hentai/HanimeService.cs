using Dynamo_Desktop.Models.Hentai;
using Dynamo_Desktop.Scrapers.Hentai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Services.Hentai
{
    public class HanimeService : HentaiService
    {
        private HanimeScraper _hanimeScraper = new HanimeScraper();
        public override Task Popular()
        {
            throw new NotImplementedException();
        }

        public async override Task<List<RecentHentai>> Recent()
        {
            try
            {
                string json = await _hanimeScraper.Recent();
                return JsonSerializer.Deserialize<List<RecentHentai>>(json);
            }
            catch
            {
                return default;
            }
        }

        public async override Task<List<RecentHentai>> Search(string Query)
        {
            try
            {
                string json = await _hanimeScraper.Search(Query);
                return JsonSerializer.Deserialize<List<RecentHentai>>(json);
            }
            catch
            {
                return default;
            }
        }
    }
}

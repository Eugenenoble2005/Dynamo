using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Scrapers.Hentai;

public class HanimeScraper
{
    private HttpClient _http = new HttpClient();
    public async void Recent()
    {
        string url = "https://hanime.tv/";
        string response = await _http.GetStringAsync(url);
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response);

        HtmlNodeCollection htv_carousels = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class,'htv-carousel ')]");
       // Debug.WriteLine(htv_carousels.Count);
        foreach(HtmlNode htv_carousel  in htv_carousels)
        {
            Debug.WriteLine(htv_carousel.GetAttributeValue("class",null));
        }
    }
}

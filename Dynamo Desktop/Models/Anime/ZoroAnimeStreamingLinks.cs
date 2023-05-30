using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Models.Anime;


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class ZoroAnimeStreamingLinks
{
	public List<ZoroAnimeStreamingSource> sources { get; set; }
	public List<ZoroAnimeStreamingSubtitle> subtitles { get; set; }
}

public class ZoroAnimeStreamingSource
{
	public string url { get; set; }
	public string quality { get; set; }
	public bool isM3U8 { get; set; }
}

public class ZoroAnimeStreamingSubtitle
{
	public string url { get; set; }
	public string lang { get; set; }
}

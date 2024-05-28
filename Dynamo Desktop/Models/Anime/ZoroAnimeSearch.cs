using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Models.Anime
{

	public class ZoroSearchResult
	{
		public string id { get; set; }
		public string title { get; set; }
		public string type { get; set; }
		public string image { get; set; }
		public string url { get; set; }
	}

	public class ZoroAnimeSearch
	{
		public string currentPage { get; set; }
		public bool hasNextPage { get; set; }
		public int totalPages { get; set; }
		public List<ZoroSearchResult> results { get; set; }
	}

}

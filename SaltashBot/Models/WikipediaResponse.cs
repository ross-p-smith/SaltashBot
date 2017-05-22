using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaltashBot.Models
{

    [JsonObject()]
    public class Article
    {
        public int PageId { get; set; }
        public int NS { get; set; }
        public string Title { get; set; }
        public string Extract { get; set; }
    }

    public class Pages
    {
        public Article Article { get; set; }
    }

    public class Query
    {
        public Pages Pages { get; set; }
    }

    public class WikipediaObject
    {
        public string BatchComplete { get; set; }
        public Query Query { get; set; }
    }
}
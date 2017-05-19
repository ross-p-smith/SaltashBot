using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaltashBot.Models
{

    public class Location
    {
        public string Type { get; set; }
        public string SameAs { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class GoogleEvent
    {
        public string Context { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Url { get; set; }
        public Location Location { get; set; }
    }

    public class Event
    {
        public string Id { get; set; }
        public string CalendarName { get; set; }
        public string CssClass { get; set; }
        public string Title { get; set; }
        public object LastUpdated { get; set; }
        public object AuthorEmailAddress { get; set; }
        public string Content { get; set; }
        public bool ContentValidHtml { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; }
        public GoogleEvent GoogleEvent { get; set; }
    }
}
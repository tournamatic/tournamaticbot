using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamaticBot
{
    public class Sport
    {
        public int CategoryId { get; set; }

        public string Title { get; set; }

        public bool IsPublished { get; set; }

        public int? ParentId { get; set; }
    }
}
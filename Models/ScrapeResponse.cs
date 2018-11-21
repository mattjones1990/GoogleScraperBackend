using System;
using System.Collections.Generic;

namespace InfoTrack.Models
{
    public class ScrapeResponse
    {
        public List<Tuple<int, string>> ListOfURLs { get; set; }
        public string ErrorReason { get; set; }
        public string GoogleURL { get; set; }
    }
}

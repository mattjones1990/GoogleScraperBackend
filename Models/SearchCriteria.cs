using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfoTrack_API.Models
{
    public class SearchCriteria
    {
        public int ResultNumber { get; set; }
        public List<string> SearchCriteriaArray { get; set; }
        public string UrlSearchString { get; set; }
        public string GoogleURL { get; set; }

        public SearchCriteria()
        {
            GoogleURL = "https://www.google.co.uk/search?num="; 
        }

        public static string generateFullURL(SearchCriteria searchCritera)
        {
            string url = searchCritera.GoogleURL;

            //Add number of searches
            url += searchCritera.ResultNumber.ToString();
            url += "&q=";

            //Add search terms
            foreach (string s in searchCritera.SearchCriteriaArray)
            {
                url += s + "+";
            }

            //Remove last '+'
            url = url.Remove(url.Length - 1);

            return url;
        }
    }
}
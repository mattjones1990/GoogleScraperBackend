using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace InfoTrack.Models
{
    public class ScrapeFactory
    {
        public static ScrapeResponse ScrapeWebsite(string url, string searchString)
        {
            ScrapeResponse sr = new ScrapeResponse();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            //Feels filthy but it does the trick
            string pattern = "(<h3 class=\"r\"><a href=\"/url\\?q=)(\\w+[a-zA-Z0-9.\\-?=/:]*)";
            MatchCollection urlCollection = Regex.Matches(doc.Text, pattern);

            //Get valid URLs
            sr.ListOfURLs = GetListWithOrder(urlCollection, searchString);

            //If no results returned, populate 'error'
            if (sr.ListOfURLs.Count == 0)
                sr.ErrorReason = "No results found";

            return sr;
        }

        private static List<Tuple<int, string>> GetListWithOrder(MatchCollection urlCollection, string urlSearchString)
        {
            List<Tuple<int, string>> list = new List<Tuple<int, string>>();
            int returnedPosition = 1;

            //Iterate through results, add valid results to tuple
            foreach (Match collection in urlCollection)
            {
                string value = collection.ToString();

                if (value.Contains(urlSearchString))
                {
                    //Remove clutter from URL
                    string cleanValue = CleanString(value);
                    list.Add(Tuple.Create(returnedPosition, cleanValue));
                }

                returnedPosition++;
            }

            return list;
        }

        private static string CleanString(string value)
        {
            string clutter = "<h3 class=\"r\"><a href=\"/url?q=";
            string cleanValue = value.Replace(clutter, "");
            return cleanValue;
        }
    }
}

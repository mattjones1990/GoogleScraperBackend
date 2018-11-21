using InfoTrack_API.Models;
using System.Linq;

namespace InfoTrack.Models
{
    public class Validation
    {
        public static string BuildUrl(SearchCriteria searchCriteria)
        {
            return "";
        }

        public static bool ValidateInput(SearchCriteria searchCritera)
        {
            if (!searchCritera.SearchCriteriaArray.Any())
                return false;

            if (searchCritera.ResultNumber < 0 || searchCritera.ResultNumber > 1000000)
                return false;

            if (searchCritera.UrlSearchString.Length < 1)
                return false;

            return true;
        }
    }
}
using InfoTrack.Models;
using InfoTrack_API.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;

namespace Matthew_Jones_InfoTrack_API.Controllers
{
    public class ScrapeController : ApiController
    {
        [Route("api/scrape")]
        [HttpPost]
        public HttpResponseMessage ScrapeWebsite([FromBody] SearchCriteria searchCriteria)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            ScrapeResponse scrapeResponse = new ScrapeResponse();

            //Validate SearchCriteria
            bool validInput = Validation.ValidateInput(searchCriteria);

            //If invalid, return error to front end.
            if (!validInput)
            {
                scrapeResponse.ErrorReason = "Invalid data supplied, ensure the fields are correctly populated with no whitespace.";
                response = Request.CreateResponse(HttpStatusCode.BadRequest, scrapeResponse);
                return response;
            }

            //Get full URL
            string createURL = SearchCriteria.generateFullURL(searchCriteria);

            //Scrape website
            scrapeResponse = ScrapeFactory.ScrapeWebsite(createURL, searchCriteria.UrlSearchString);
            scrapeResponse.GoogleURL = createURL;

            //Either return successfull data, or error
            response = Request.CreateResponse(HttpStatusCode.OK, scrapeResponse);
            return response;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySampleApplication.Helpers;
using MySampleApplication.Models.Request;
using MySampleApplication.Models.Responses;

namespace MySampleApplication.Controllers.Api
{
    [RoutePrefix("api/webscraper")]
    public class WebScraperController : ApiController
    {
        [HttpPost,AllowAnonymous]
        public HttpResponseMessage GetData(WebScrapeAddRequest model)
        {
            WebScraper scraper = new WebScraper(model.Url);
            ItemResponse<string> resp = new ItemResponse<string>();
            resp.Item = scraper.GetContent("plan");
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, resp);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySampleApplication.Helpers;
using MySampleApplication.Models.Request;
using MySampleApplication.Models.Responses;
using MySampleApplication.Models.ViewModel;

namespace MySampleApplication.Controllers.Api
{
    [RoutePrefix("api/webscraper")]
    public class WebScraperController : ApiController
    {
        [Route,HttpGet,AllowAnonymous]
        public HttpResponseMessage GetData()
        {

            WebScraper scraper = new WebScraper();
            ItemsResponse<WebScrapeViewModel> resp = new ItemsResponse<WebScrapeViewModel>();
            resp.Items = scraper.GetContent();
            //resp.Item = new WebScrapeViewModel();
            //resp.Item.ImageUrl = scraper.GetContent("//source");
            //resp.Item.Title = scraper.GetContent("//figcaption");
            //resp.Item.Description = scraper.GetContent("//figcaption/span");

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, resp);
        }
    }
}

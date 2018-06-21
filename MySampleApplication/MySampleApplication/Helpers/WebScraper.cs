using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using MySampleApplication.Models.ViewModel;

namespace MySampleApplication.Helpers
{
    public class WebScraper
    {
        public bool IsPageLoaded = false;
        public string GetUrl = "https://www.bodybuilding.com/workout-plans/level/beginner";

        private HtmlDocument _htmlDocument;

        public List<WebScrapeViewModel> GetContent()
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load(GetUrl);

            if (!this.IsPageLoaded)
            {
                this.loadHtml();
            }

            var tags = _htmlDocument.DocumentNode.SelectNodes("//*[contains(@class,'plan ')]");
            List<WebScrapeViewModel> contentList = new List<WebScrapeViewModel>();
            foreach (var item in tags)
            {
                WebScrapeViewModel model = new WebScrapeViewModel();
                model.ImageUrl = item.SelectSingleNode(".//img").GetAttributeValue("srcset", "not found");
                model.Title = item.SelectSingleNode(".//span[@class='plan__info__value plan__info__value--bold']").InnerHtml;
                model.Description = item.SelectSingleNode("//span[@class='plan__info__value']").InnerHtml + " " +
                    item.SelectSingleNode("//span[@class='plan__info__delimiter']").InnerHtml + " " +
                    item.SelectSingleNode("//*[@id='FAWP_PLAN_THUMBNAIL_0']/figcaption/div[2]/span[3]").InnerHtml;
                model.titleLink = item.SelectSingleNode(".//a[@class='plan__link']").GetAttributeValue("href", "nada");
                contentList.Add(model);
            }
            return contentList;
        }
        private void loadHtml()
        {
            HtmlWeb htmlweb = new HtmlWeb();
            _htmlDocument = htmlweb.Load(GetUrl);
            IsPageLoaded = true;
        }
        public WebScraper()
            {

            }
        public WebScraper(string url)
        {

            this.GetUrl = url;
        }
    }
}
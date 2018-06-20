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

        public List<string> GetContent(string propName)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load(GetUrl);

            if (!this.IsPageLoaded)
            {
                this.loadHtml();
            }

            var tags = _htmlDocument.DocumentNode.SelectNodes("//div");
            List<string> content = new List<string>();
            foreach (var tag in tags)
            {
                if (propName == "//source")
                {
                    var test = tag.SelectNodes(propName);
                    foreach (var item in test)
                    {
                        var o = item.Attributes["srcset"].Value;
                        content.Add(o);
                    }
                    break;
                }
                if (propName == "//figcaption")
                {
                    var title = tag.SelectNodes(propName);
                    foreach (var item in title)
                    {
                        var p = item.SelectNodes("//span[@class='plan__info__value plan__info__value--bold']");
                        foreach (var pp in p)
                        {
                            var w = pp.InnerHtml;
                            content.Add(w);
                        }
                        break;
                    }

                    break;
                }
                if (propName == "//figcaption/span")
                {
                    var desc = tag.SelectNodes("//figcaption");
                    foreach (var item in desc)
                    {
                        var p = item.SelectNodes("//div[@class='plan__info']");
                        foreach (var pp in p)
                        {
                            var w = pp.InnerText;
                            content.Add(w);
                        }
                        break;
                    }

                    break;
                }
            }
                return content;
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
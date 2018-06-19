using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace MySampleApplication.Helpers
{
    public class WebScraper
    {
        public bool IsPageLoaded = false;
        public string GetUrl { get; set; }

        private HtmlDocument _htmlDocument;

        public string GetContent(string propName)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load(GetUrl);

            if (!this.IsPageLoaded)
            {
                this.loadHtml();
            }

            HtmlNode tags = _htmlDocument.DocumentNode.SelectSingleNode("//div");
            foreach (HtmlNode tag in tags.SelectNodes("//figure/a"))
            {
                var test = tag.SelectSingleNode("//source");
                var um = test.Attributes["srcset"].Value;
                
               
            }
            string content = string.Empty;
            //foreach (var item in tags)
            //{
            //    string prop = item.GetAttributeValue("class","");
            //    if (!string.IsNullOrWhiteSpace(prop) && prop == propName)
            //    {
            //        content = item.GetAttributeValue("content", "");
            //        if (!string.IsNullOrEmpty(content))
            //        {
            //            break;
            //        }
            //    }
            //}
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySampleApplication.Models.ViewModel
{
   public class WebScrapeViewModel
    {
        public List<string> ImageUrl { get; set; }
        public List<string> Title { get; set; }
        public List<string> Description { get; set; }
    }
}

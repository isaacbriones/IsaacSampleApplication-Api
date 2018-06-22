using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySampleApplication.Models.Request
{
    public class SupplementAddRequest
    {
        public int Id { get; set; }
        public string SupplementName { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }
}

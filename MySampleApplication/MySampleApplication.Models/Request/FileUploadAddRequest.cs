using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySampleApplication.Models.Request
{
    public class FileUploadAddRequest
    {
        public int Id { get; set; }
        public int FileTypeId { get; set; }
        public string UserFileName { get; set; }
        public string SystemFileName { get; set; }
        public string Location { get; set; }
    }
}

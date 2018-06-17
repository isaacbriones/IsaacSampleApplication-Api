using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySampleApplication.Models.Responses
{
    public class LoginResponse<T> : ItemResponse<T>
    {
        public List<int> PersonID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppDomain.Helper
{
    public class ServiceResponse<T>
    {
        public string Code { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public string ValidationError { get; set; }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Result
    {
        public bool Correct { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Excepcion { get; set; }
        public List<object> Objects { get; set; } = new List<object>();
        public List<string> Strings { get; set; } = new List<string>();
        public object Object { get; set; }
    }
}

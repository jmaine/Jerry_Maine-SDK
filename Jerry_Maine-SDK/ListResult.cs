using System;
using System.Collections.Generic;
using System.Text;

namespace Jerry.Maine.SDK
{
    public class ListResult<T>
    {
        public List<T>? Docs { get; set; }
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }
    }
}

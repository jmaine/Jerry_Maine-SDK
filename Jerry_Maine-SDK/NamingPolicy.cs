using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Jerry.Maine.SDK
{
    internal class NamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return name;
        }
    }
}

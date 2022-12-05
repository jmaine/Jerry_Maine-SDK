using System;
using System.Collections.Generic;
using System.Text;

namespace Jerry.Maine.SDK
{
    public class Match : IFilter
    {
        public bool Negate { get; set; }
        public string Field { get; set; } = "";
        public List<string?> List { get; set; } = new List<string?>();

        public string Name { 
            get { 
                if (Negate && (List?.Count ?? 0) != 0 )
                {
                    return Field + "!";
                }
                else if (Negate)
                {
                    return  "!" + Field;
                } 
                else
                {
                    return Field;
                }
            } 
        }

        public string Value => String.Join(",", List);
    }
}

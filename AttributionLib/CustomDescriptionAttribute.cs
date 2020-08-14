using System;
using System.Collections.Generic;
using System.Text;

namespace AttributionLib
{
    public class CustomDescriptionAttribute : Attribute
    {
        public string Description { get; set; }
        public CustomDescriptionAttribute(string description)
        {
            Description = description;
        }

    }
}

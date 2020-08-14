using System;

namespace AttributionLib
{
    public class AllowedRangeAttribute : Attribute
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public AllowedRangeAttribute(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

    }
}

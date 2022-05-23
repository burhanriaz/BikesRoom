using System;
using System.ComponentModel.DataAnnotations;

namespace BikesRoom.Extentions
{
    public class YearRangeAttribute : RangeAttribute
    
    {
        public YearRangeAttribute(int StartYear):base (StartYear,DateTime.Today.Year)
        {

        }

    }
}

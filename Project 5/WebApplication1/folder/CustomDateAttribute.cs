using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.folder
{
    public class CustomDateAttribute : RangeAttribute
    {
        public CustomDateAttribute()
          : base(typeof(DateTime),
                  new DateTime(1900,1,1).ToShortDateString(),
                  new DateTime(9999, 1, 1).ToShortDateString())
        { }
    }
}
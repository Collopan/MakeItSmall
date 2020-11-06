using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MakeItSmall.Models
{
    public class URLModel
    {        
        public String BIG_URL { get; set; }
        public String SMALL_URL { get; set; }
    }
}

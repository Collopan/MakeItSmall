using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeItSmall.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace MakeItSmall.Controllers
{
    public class RedirectionController : Controller
    {
        
        public IActionResult Redirection(string tempURL)
        {
            return Redirect(tempURL);            
        }
    }
}

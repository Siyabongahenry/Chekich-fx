using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chekich_fx.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "writepolicy")]
    public class HomeController : Controller
    {    
        public IActionResult Index()
        {
            return RedirectToAction("Index","Product");
        }
    }
}

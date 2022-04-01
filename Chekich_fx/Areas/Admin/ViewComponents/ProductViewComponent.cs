using Chekich_fx.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.Areas.Admin.ViewComponents
{
    public class ProductViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(Product product)
        { 
            return View(product);
        }
    }
}

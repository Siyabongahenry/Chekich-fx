using Chekich_fx.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.ViewComponents
{
    public class ListAddressViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(List<Address> _address)
        {
            if (_address != null)
            {
                return View(_address);
            }
            else
            {
                return View(new List<Address>());
            }
        }
    }
}

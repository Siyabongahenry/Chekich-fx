using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Chekich_fx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chekich_fx.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class EmailController : Controller
    {    
       
        public EmailController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        
    }
}

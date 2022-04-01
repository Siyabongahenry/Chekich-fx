using Chekich_fx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.Areas.Admin.Models
{
    public class ApplicationUserRole
    {
        public ApplicationUser User { get; set; }
        public string Role{ get; set; }
    }
}

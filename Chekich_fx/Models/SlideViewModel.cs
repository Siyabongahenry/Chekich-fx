using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.Models
{
    public class SlideViewModel
    {
        public FileInfo[] MobileFiles { get; set; }
        public FileInfo[] TabletFiles { get; set; }
        public FileInfo[] DesktopFiles { get; set; }
    }
}

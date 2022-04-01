using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.Models
{
    public class FileManangerModel
    {
        public FileInfo[] Files { get; set; }
        public IFormFile FormFile { get; set; }
        public List<IFormFile> IFormFiles { get; set; }
        public int ProdId { get; set; }
    }
}

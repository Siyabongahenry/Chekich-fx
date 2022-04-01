using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.Models
{
    public class ImageUpload
    {   
        public static string UploadImage(IWebHostEnvironment _enviroment,IFormFile _File,string _path)
        {
            string uniqueFileName = null;
            if (_File != null)
            {
                string uploadsFolder = Path.Combine(_enviroment.WebRootPath, _path);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + _File.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    _File.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}

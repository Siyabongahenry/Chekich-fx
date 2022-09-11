using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Chekich_fx.Utility
{
    public static class FileManager
    {

        public static string Upload(IFormFile File,string uploadPath)
        {
            if (File == null && uploadPath == null) return null;

            //create a unique name for the file
            string uniqueFileName = Guid.NewGuid().ToString() + "_" +File.Name;

            //create the final path for the file
            string filePath = Path.Combine(uploadPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                File.CopyTo(fileStream);
            }

            //return the unique name of the file
            return uniqueFileName;
        }

        public static void Delete(string filePath)
        {

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        
        }
    }
}

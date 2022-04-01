using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Chekich_fx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chekich_fx.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "writepolicy")]
    [AutoValidateAntiforgeryToken]
    public class SlideController : Controller
    {
        private IWebHostEnvironment _enviroment;
        public SlideController(IWebHostEnvironment enviroment)
        {
            _enviroment = enviroment;

        }
        public IActionResult Index()
        {
            FileManangerModel model = new FileManangerModel();
            string slideFolder = Path.Combine(_enviroment.WebRootPath, "images", "slide");
            DirectoryInfo dir = new DirectoryInfo(slideFolder);
            FileInfo[] files = dir.GetFiles();
            model.Files = files;
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(IFormFile FormFile)
        {
            ViewBag.ImageError = "";
            ViewBag.ImageUploaded = "";
            if (UploadSlideImage(FormFile) != null)
            {
                ViewBag.ImageUploaded = "Uploaded succesfully..";
            }
            else
            {
                ViewBag.ImageError = "Something went wrong..";
            }    
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Delete(string filename)
        {
            DeleteSlideImage(filename);
            return RedirectToAction(nameof(Index));
        }
        private string UploadSlideImage(IFormFile File)
        {
            string uniqueFileName = null;
            if (File != null)
            {
                string uploadsFolder = Path.Combine(_enviroment.WebRootPath, "images", "slide");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + File.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    File.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private void DeleteSlideImage(string _fileName)
        {
            string uploadsFolder = Path.Combine(_enviroment.WebRootPath, "images", "slide");
            string filePath = Path.Combine(uploadsFolder, _fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}

using ImageGallery.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Image.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ImageGallery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = context;
            _hostEnvironment=hostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Imagedata> objCatlist = _context.Images;
            return View(objCatlist);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Image()
        {
            IEnumerable<Imagedata> objCatlist = _context.Images;
            return View(objCatlist);
        }

        public IActionResult Details(int? id)
        {
            //IEnumerable<Imagedata> objCatlist = _context.Images;
            //return View(objCatlist);

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.Images.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Imagedata empobj)
        {
            if (ModelState.IsValid)
            {
                //Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileName(empobj.Image.FileName);
                //string extension = Path.GetExtension(empobj.Image.FileName);
                //empobj.ImageName=fileName = fileName + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await empobj.Image.CopyToAsync(fileStream);
                }
                //Insert record
                _context.Add(empobj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Image));
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.Images.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Imagedata empobj)
        {
            if (ModelState.IsValid)
            {
                _context.Images.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction("Image");
            }

            return View(empobj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.Images.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var imageModel = await _context.Images.FindAsync(id);

            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", imageModel.ImageUrl);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            //delete the record
            _context.Images.Remove(imageModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Image));
        }
    }
}
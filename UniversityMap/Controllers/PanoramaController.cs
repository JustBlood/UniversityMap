using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using UniversityMap.Data;
using UniversityMap.Models;

namespace UniversityMap.Controllers
{
    public class PanoramaController : Controller
    {
        private readonly UniversityMapContext _context;
        public PanoramaController(UniversityMapContext context)
        {
            _context = context;
        }
        public IActionResult Index(string? tag)
        {
            Panorama panorama;

            if (tag == null)
                panorama = _context.Panoramas.FirstOrDefault();
            else
                panorama = _context.Panoramas.SingleOrDefault(x => x.Tag == tag);

            if (panorama != null)
            {
                var PanoramaOptionsScript = CreatePanoramaOptions(panorama.Id);
                ViewData["PanoramaOptionsScript"] = PanoramaOptionsScript;
            }
            // Сохраняет временный файлик, изменяющий ссылку на изображение функцией CreatePanoramaOptions
            //System.IO.File.Delete("wwwroot/js/panorama/panorama-options-temp.js");
            //System.IO.File.WriteAllText("wwwroot/js/panorama/panorama-options-temp.js", PanoramaOptionsScript);

            return View(panorama);
        }

        public FileContentResult Show(int? id)
        {
            var panorama = _context.Panoramas.FirstOrDefault(p => p.Id == id);
            if (panorama != null)
                return File(panorama.Image, "image/jpg");
            return null;
        }

        private string CreatePanoramaOptions(int id)
        {
            var script = System.IO.File.ReadAllText("wwwroot/js/panorama/panorama-options.js");
            return script.Replace("+", Url.Action("show", new { id }));
        }
    }
}

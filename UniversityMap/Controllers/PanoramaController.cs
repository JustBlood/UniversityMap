using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
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
                var PanoramaOptionsScript = CreatePanoramaOptions(panorama.Id, panorama);
                ViewData["PanoramaOptionsScript"] = PanoramaOptionsScript;
            }

            return View(panorama);
        }

        public FileContentResult Show(int? id)
        {
            var panorama = _context.Panoramas.FirstOrDefault(p => p.Id == id);
            if (panorama != null)
                return File(panorama.Image, "image/jpg");
            return null;
        }

        private string CreatePanoramaOptions(int id, Panorama panorama)
        {
            //var script = System.IO.File.ReadAllText("wwwroot/js/panorama/panorama-options.js");
            //script = script.Replace("{{ scenes }}", panorama.PanoramaOptionsScript); // добавляем сцену
            //return script.Replace("+", Url.Action("show", new { id }));
            var sb = new StringBuilder();
            sb.Append(System.IO.File.ReadAllText("wwwroot/js/panorama/panorama-options.js"));
            sb.Replace("{{ scenes }}", panorama.PanoramaOptionsScript);
            sb.Replace("+", Url.Action("show", new { id }));
            // костыль, который переделывает наш скрипт для вставки ссылок на соседние панорамы, если они есть.
            sb.Replace("{{ left }}", $"/panorama?tag={panorama.Left}");
            sb.Replace("{{ top }}", $"/panorama?tag={panorama.Top}");
            sb.Replace("{{ right }}", $"/panorama?tag={panorama.Right}");
            sb.Replace("{{ bottom }}", $"/panorama?tag={panorama.Bottom}");
            return sb.ToString();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityMap.Data;

namespace UniversityMap.Controllers
{
    public class MapsController : Controller
    {
        private readonly UniversityMapContext _context;
        public MapsController(UniversityMapContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // показывает страницу карты
            ViewData["Url"] = $"/Maps";
            ViewData["Names"] = _context.Maps
                .Select(x => x.Name)
                .Distinct()
                .ToArray();
            var cfuMap = _context.Maps
                .FirstOrDefault(x => x.Name == "CFU");
            return View(cfuMap);
        }

        [Route("/Maps/{name}")]
        public IActionResult Building(string name, int floor = 1)
        {
            var map = _context.Maps
                .SingleOrDefault(x => x.Name == name
                && x.Floor == floor);
            ViewData["Name"] = name;
            ViewData["Url"] = $"/Maps/{name}/";
            ViewData["Floors"] = _context.Maps
                .Where(x => x.Name == name)
                .Select(x => x.Floor)
                .Distinct()
                .ToArray();
            return View(map);
        }
    }
}

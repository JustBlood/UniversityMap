using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ViewData["Buildings"] = _context.Maps
                .Select(x => x.Building)
                .Distinct()
                .ToArray();
            return View();
        }

        [Route("/Maps/{building}")]
        public IActionResult Building(char building, int floor = 1)
        {
            var map = _context.Maps
                .SingleOrDefault(x => x.Building == building
                && x.Floor == floor);
            ViewData["Building"] = building;
            ViewData["Url"] = $"/Maps/{building}";
            ViewData["Floors"] = _context.Maps
                .Select(x => x.Floor)
                .Distinct()
                .ToArray();
            return View(map);
        }
    }
}

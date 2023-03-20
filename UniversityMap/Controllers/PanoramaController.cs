using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityMap.Data;

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
            if (tag == null)
                return View(_context.Panoramas.FirstOrDefault());
            
            var panorama = _context.Panoramas.SingleOrDefault(x => x.Tag == tag);
            return View(panorama);
        }
    }
}

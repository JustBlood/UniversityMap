using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UniversityMap.Data;
using UniversityMap.Models;

namespace UniversityMap.Controllers
{
    public class PanoramasCRUDController : Controller
    {
        private readonly UniversityMapContext _context;
        public PanoramasCRUDController(UniversityMapContext context)
        {
            _context = context;
        }

        // GET: Panoramas
        public async Task<IActionResult> Index()
        {
            // переделать под json?
            var universityMapContext = _context.Panoramas.Include(p => p.Map);
            return View(await universityMapContext.ToListAsync());
        }

        // GET: Panoramas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Panoramas == null)
            {
                return NotFound();
            }

            var panorama = await _context.Panoramas
                .Include(p => p.Map)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panorama == null)
            {
                return NotFound();
            }

            return View(panorama);
        }

        // GET: Panoramas/Create
        public IActionResult Create()
        {
            ViewData["MapId"] = new SelectList(_context.Maps, "Id", "Id");
            return View();
        }

        // POST: Panoramas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Tag,Image,Left,Top,Right,Bottom,MapId")] Panorama panorama)
        //{
        //    if (ModelState.IsValid)
        //    {


        //        _context.Add(panorama);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["MapId"] = new SelectList(_context.Maps, "Id", "Id", panorama.MapId);
        //    return View(panorama);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FileUpload fileObj)
        {
            var panorama = fileObj.CreatePanorama();
            if (panorama != null)
            {
                _context.Panoramas.Add(panorama);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["MapId"] = new SelectList(_context.Maps, "Id", "Id", panorama.MapId);
            return View(panorama);
        }

        // GET: Panoramas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Panoramas == null)
            {
                return NotFound();
            }

            var panorama = await _context.Panoramas.FindAsync(id);
            if (panorama == null)
            {
                return NotFound();
            }
            ViewData["MapId"] = new SelectList(_context.Maps, "Id", "Id", panorama.MapId);
            return View(panorama);
        }

        // POST: Panoramas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FileUpload fileObj)
        {
            var panorama = fileObj.CreatePanorama();
            if (id != panorama.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(panorama);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PanoramaExists(panorama.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MapId"] = new SelectList(_context.Maps, "Id", "Id", panorama.MapId);
            return View(panorama);
        }

        // GET: Panoramas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Panoramas == null)
            {
                return NotFound();
            }

            var panorama = await _context.Panoramas
                .Include(p => p.Map)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panorama == null)
            {
                return NotFound();
            }

            return View(panorama);
        }

        // POST: Panoramas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Panoramas == null)
            {
                return Problem("Entity set 'UniversityMapContext.Panoramas'  is null.");
            }
            var panorama = await _context.Panoramas.FindAsync(id);
            if (panorama != null)
            {
                _context.Panoramas.Remove(panorama);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PanoramaExists(int id)
        {
            return (_context.Panoramas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

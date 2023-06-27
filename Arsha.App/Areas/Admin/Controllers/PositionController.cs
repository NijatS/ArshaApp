using Arsha.App.Context;
using Arsha.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Arsha.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private readonly ArshaAppDbContext _context;

        public PositionController(ArshaAppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable <Position> positions = await _context.Positions.
                Where(x => !x.IsDeleted).ToListAsync();

            return View(positions);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Position position)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            position.CreatedDate = DateTime.Now;
            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Position");
        }

        public async Task<IActionResult> Update(int id)
        {
            Position? position = await _context.Positions.Where(x=> !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if(position is null)
            {
                return NotFound();
            }
            return View(position);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,Position position)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Position? updatedPosition = await _context.Positions.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (position is null)
            {
                return NotFound();
            }
            updatedPosition.UpdatedDate = DateTime.Now;
            updatedPosition.Name = position.Name;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            Position? position = await _context.Positions.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (position is null)
            {
                return NotFound();
            }
            position.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

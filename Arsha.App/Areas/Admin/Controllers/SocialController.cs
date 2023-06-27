using Arsha.App.Context;
using Arsha.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arsha.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialController : Controller
    {
        private readonly ArshaAppDbContext _context;

        public SocialController(ArshaAppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Social> socials = await _context.Socials.
                Include(x=>x.Team).
                Where(x => !x.IsDeleted).ToListAsync();

            return View(socials);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Teams = await _context.Teams.Where(x => !x.IsDeleted).ToListAsync();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Social social)
        {
            ViewBag.Teams = await _context.Teams.Where(x => !x.IsDeleted).ToListAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }
            social.CreatedDate = DateTime.Now;
            await _context.Socials.AddAsync(social);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Social");
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Teams = await _context.Teams.Where(x => !x.IsDeleted).ToListAsync();

            Social? social = await _context.Socials.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (social is null)
            {
                return NotFound();
            }
            return View(social);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Social social)
        {
            ViewBag.Teams = await _context.Teams.Where(x => !x.IsDeleted).ToListAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }
            Social? updatedSocial = await _context.Socials.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (social is null)
            {
                return NotFound();
            }
            updatedSocial.UpdatedDate = DateTime.Now;
            updatedSocial.TeamId = social.TeamId;
            updatedSocial.Icon = social.Icon;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            Social? social = await _context.Socials.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (social is null)
            {
                return NotFound();
            }
            social.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}


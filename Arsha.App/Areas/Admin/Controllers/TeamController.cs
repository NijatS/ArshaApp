using Arsha.App.Context;
using Arsha.App.Extensions;
using Arsha.App.Helpers;
using Arsha.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arsha.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly ArshaAppDbContext _context;
        private IWebHostEnvironment _evm;

        public TeamController(IWebHostEnvironment evm, ArshaAppDbContext context)
        {
            _evm = evm;
            _context = context;

        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Team> teams = await _context.Teams.
                Include(x=> x.Socials).
                Include(x => x.Position).
                Where(x=> !x.IsDeleted).ToListAsync();
            return View(teams);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Position = await _context.Positions.Where(x => !x.IsDeleted).ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
        {
            ViewBag.Position = await _context.Positions.Where(x => !x.IsDeleted).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }
            if(team.file is null)
            {
                ModelState.AddModelError("file", "Image is required");
                return View();
            }
            if (!Helper.isImage(team.file))
            {
                ModelState.AddModelError("file", "Image is required");
                return View();
            }
            if (!Helper.isSize(team.file,1))
            {
                ModelState.AddModelError("file", "Image size is less than 1 mb");
                return View();
            }
            team.CreatedDate = DateTime.Now;
            team.Photo = team.file.CreateImage(_evm.WebRootPath, "assets/img/team/");
            team.Position = _context.Positions.Where(x => x.Id == team.PositionId).FirstOrDefault();
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Team");
        }
        public async Task<IActionResult> Update(int id)
        {
            Team? team = await _context.Teams.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            ViewBag.Position = await _context.Positions.Where(x => !x.IsDeleted).ToListAsync();

            if (team is null)
            {
                return NotFound();
            }
            return View(team);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Team team)
        {
            Team? updatedteam = await _context.Teams.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            ViewBag.Position = await _context.Positions.Where(x => !x.IsDeleted).ToListAsync();

            if (team is null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(updatedteam);
            }
            if (team.file is not null)
            {
                if (!Helper.isImage(team.file))
                {
                    ModelState.AddModelError("file", "Image is required");
                    return View();
                }
                if (!Helper.isSize(team.file, 1))
                {
                    ModelState.AddModelError("file", "Image size is less than 1 mb");
                    return View();
                }
                updatedteam.Photo = team.file.CreateImage(_evm.WebRootPath, "assets/img/team/");
            }
       
            updatedteam.UpdatedDate = DateTime.Now;
            updatedteam.FullName = team.FullName;
            updatedteam.PositionId = team.PositionId;
            updatedteam.Description = team.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            Team? team = await _context.Teams.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (team is null)
            {
                return NotFound();
            }
            team.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

using Arsha.App.Context;
using Arsha.App.Extensions;
using Arsha.App.Helpers;
using Arsha.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arsha.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioItemController : Controller
    {
        private readonly ArshaAppDbContext _context;
        private IWebHostEnvironment _evm;

        public PortfolioItemController(IWebHostEnvironment evm, ArshaAppDbContext context)
        {
            _evm = evm;
            _context = context;

        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<PortfolioItem> portfolioItems = await _context.PortfolioItems.
                Include(x => x.PortfolioCategory).
                Where(x => !x.IsDeleted).ToListAsync();
            return View(portfolioItems);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.PortfolioCate = await _context.PortfolioCategories.Where(x => !x.IsDeleted).ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortfolioItem item)
        {
            ViewBag.PortfolioCate = await _context.PortfolioCategories.Where(x => !x.IsDeleted).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (item.file is null)
            {
                ModelState.AddModelError("file", "Image is required");
                return View();
            }
            if (!Helper.isImage(item.file))
            {
                ModelState.AddModelError("file", "Image is required");
                return View();
            }
            if (!Helper.isSize(item.file, 1))
            {
                ModelState.AddModelError("file", "Image size is less than 1 mb");
                return View();
            }
            item.CreatedDate = DateTime.Now;
            item.Photo = item.file.CreateImage(_evm.WebRootPath, "assets/img/portfolio/");
            item.PortfolioCategory = _context.PortfolioCategories.Where(x => x.Id == item.PortfolioCategoryId).FirstOrDefault();
            await _context.PortfolioItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "PortfolioItem");
        }
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.PortfolioCate = await _context.PortfolioCategories.Where(x => !x.IsDeleted).ToListAsync();
            PortfolioItem? item = await _context.PortfolioItems.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (item is null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, PortfolioItem item)
        {
            ViewBag.PortfolioCate = await _context.PortfolioCategories.Where(x => !x.IsDeleted).ToListAsync();
            PortfolioItem? updatedItem = await _context.PortfolioItems.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();

            if (item is null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(updatedItem);
            }
            if (item.file is not null)
            {
                if (!Helper.isImage(item.file))
                {
                    ModelState.AddModelError("file", "Image is required");
                    return View();
                }
                if (!Helper.isSize(item.file, 1))
                {
                    ModelState.AddModelError("file", "Image size is less than 1 mb");
                    return View();
                }
                updatedItem.Photo = item.file.CreateImage(_evm.WebRootPath, "assets/img/portfolio/");
            }

            updatedItem.UpdatedDate = DateTime.Now;
            updatedItem.Name = item.Name;
            updatedItem.PortfolioCategoryId = item.PortfolioCategoryId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            PortfolioItem? item = await _context.PortfolioItems.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (item is null)
            {
                return NotFound();
            }
            item.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

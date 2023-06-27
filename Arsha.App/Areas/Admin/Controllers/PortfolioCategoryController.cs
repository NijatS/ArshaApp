using Arsha.App.Context;
using Arsha.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Arsha.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioCategoryController : Controller
    {
        private readonly ArshaAppDbContext _context;

        public PortfolioCategoryController(ArshaAppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable <PortfolioCategory> portfolioCategories = await _context.PortfolioCategories.
                Where(x => !x.IsDeleted).ToListAsync();

            return View(portfolioCategories);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortfolioCategory portfolioCategory)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            portfolioCategory.CreatedDate = DateTime.Now;
            await _context.PortfolioCategories.AddAsync(portfolioCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            PortfolioCategory? portfolioCategory = await _context.PortfolioCategories.Where(x=> !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if(portfolioCategory is null)
            {
                return NotFound();
            }
            return View(portfolioCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,PortfolioCategory portfolioCategory)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            PortfolioCategory? updatedPortfolioCa = await _context.PortfolioCategories.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (portfolioCategory is null)
            {
                return NotFound();
            }
            updatedPortfolioCa.UpdatedDate = DateTime.Now;
            updatedPortfolioCa.Name = portfolioCategory.Name;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            PortfolioCategory? portfolioCategory = await _context.PortfolioCategories.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (portfolioCategory is null)
            {
                return NotFound();
            }
            portfolioCategory.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

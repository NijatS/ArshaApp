using Arsha.App.Context;
using Arsha.App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arsha.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArshaAppDbContext _context;

        public HomeController(ArshaAppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel homeView = new HomeViewModel();
            homeView.Services = await _context.Services.Where(x=>!x.IsDeleted).ToListAsync();
            homeView.Teams = await _context.Teams.
                Include(x=>x.Socials).
                Include(x=>x.Position).
                Where(x=>!x.IsDeleted).ToListAsync();
            homeView.Categories = await _context.PortfolioCategories.
                Where(x => !x.IsDeleted).ToListAsync();
            homeView.Items = await _context.PortfolioItems.
                Include(x=>x.PortfolioCategory).
                Where(x => !x.IsDeleted).ToListAsync();
            return View(homeView);
        }
    }
}

using Arsha.App.Context;
using Arsha.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Arsha.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly ArshaAppDbContext _context;

        public ServiceController(ArshaAppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable <Service> services = await _context.Services.
                Where(x => !x.IsDeleted).ToListAsync();

            return View(services);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            service.CreatedDate = DateTime.Now;
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Service");
        }

        public async Task<IActionResult> Update(int id)
        {
            Service? service = await _context.Services.Where(x=> !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if(service is null)
            {
                return NotFound();
            }
            return View(service);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,Service service)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Service? updateService = await _context.Services.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (service is null)
            {
                return NotFound();
            }
            updateService.UpdatedDate = DateTime.Now;
            updateService.Title = service.Title;
            updateService.Description = service.Description;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Service");
        }
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            Service? service = await _context.Services.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (service is null)
            {
                return NotFound();
            }
            service.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

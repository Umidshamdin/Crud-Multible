using BasketTask.Data;
using BasketTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTask.Areas.AdminArea.Controllers
{

    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.AsNoTracking().Where(m => !m.IsDeleted).ToListAsync();
            return View(categories);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExist = _context.Categories.Any(m => m.Name.ToLower().Trim() == category.Name.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu category artiq movcuddur");
                return View();
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            Category category = await _context.Categories.Where(m => !m.IsDeleted && m.Id == id).FirstOrDefaultAsync();
            
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,Category category)
        {
            if (!ModelState.IsValid) return View();




            if (id != category.Id) return NotFound();




            Category dbCategory = await _context.Categories.AsNoTracking().Where(m => !m.IsDeleted && m.Id == id).FirstOrDefaultAsync();


            if (dbCategory.Name.ToLower().Trim() == category.Name.ToLower().Trim())
            {
                return RedirectToAction(nameof(Index));
            }




            bool isExist = _context.Categories.Where(m => m.IsDeleted).Any(m => m.Name.ToLower().Trim() == category.Name.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu kategory artiq movcuddur");
                return View();
                  

            }




            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }



       

    }

}

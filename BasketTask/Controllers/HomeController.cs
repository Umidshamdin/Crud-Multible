using BasketTask.Data;
using BasketTask.Models;
using BasketTask.Services;
using BasketTask.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly LayoutService _layoutservice;

        public HomeController(AppDbContext context,LayoutService layoutService)
        {
            _context = context;
            _layoutservice = layoutService;

        }


        public async Task<IActionResult> Index()
        {
            List<DesignCard> designCards = await _context.DesignCards.ToListAsync();
            List<OwlCarusel> owlCarusels = await _context.OwlCarusels.ToListAsync();
            List<Setting> settings = await _context.Settings.ToListAsync();
            List<Category> categories = await _context.Categories.ToListAsync();

            List<Slider> sliders = await _context.Sliders.ToListAsync();
        

            HomeVM homeVM = new HomeVM
            {
                DesignCards=designCards,
                OwlCarusels=owlCarusels,
                Settings=settings,
                Categories=categories,
                Sliders=sliders
                
                
                
            };
           

            return View(homeVM);
        }

      
    }
}

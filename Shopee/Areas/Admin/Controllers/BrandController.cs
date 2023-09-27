using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopee.Models;
using Shopee.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shopee.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BrandController : Controller
    {
        // GET: /<controller>/

        private readonly DataContext _dataContext;

        public BrandController(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IActionResult> Index()
        {
            List<BrandModel> categories = await _dataContext.Brands.OrderByDescending(p => p.Id).ToListAsync();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandModel brand)
        {

            if (ModelState.IsValid)
            {
                brand.Slug = brand.Name.ToLower().Replace(" ", "_");
                var Slug = await _dataContext.Categories.FirstOrDefaultAsync(p => p.Slug == brand.Slug);

                if (Slug != null)
                {
                    ModelState.AddModelError("", " Brand already exists");
                    return View(brand);
                }
                _dataContext.Add(brand);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Brand added successfully";
                return RedirectToAction("Index");
            }

            else
            {
                TempData["error"] = "Please double check the fields";
                List<string> errors = new List<string>();
                foreach (var values in ModelState.Values)
                {
                    foreach (var error in values.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string messsage = string.Join("\n", errors);
                return BadRequest(messsage);
            }

        }


    }
}


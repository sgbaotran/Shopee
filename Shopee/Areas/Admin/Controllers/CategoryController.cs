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
    [Area("admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly DataContext _dataContext;

        public CategoryController(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["title"] = "Create Category";
            return View(await _dataContext.Categories.OrderByDescending(p => p.Id).ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryModel category)
        {

            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "_");
                var Slug = await _dataContext.Categories.FirstOrDefaultAsync(p => p.Slug == category.Slug);

                if (Slug != null)
                {
                    ModelState.AddModelError("", " Category already exists");
                    return View(category);
                }
                _dataContext.Add(category);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Category added successfully";
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



        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }



    }
}


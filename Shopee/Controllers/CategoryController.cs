using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopee.Models;
using Shopee.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shopee.Controllers
{
    public class CategoryController : Controller
    {

        private readonly DataContext _dataContext;

        public CategoryController(DataContext context)
        {
            _dataContext = context;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(string Slug = "")
        {
            TempData["slug"] = Slug;
            CategoryModel category = _dataContext.Categories.Where(c => c.Slug == Slug).FirstOrDefault();
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var productsByCategory = _dataContext.Products.Where(row => row.CategoryId == category.Id);
                return View(await productsByCategory.OrderByDescending(c=>c.Id).ToListAsync());
            }
        }


    }
}


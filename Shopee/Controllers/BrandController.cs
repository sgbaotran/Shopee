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
    public class BrandController : Controller
    {
        private readonly DataContext _dataContext;

        public BrandController(DataContext context)
        {
            _dataContext = context;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(string Slug = "", bool deptraikhong = true)
        {
            TempData["slug"] = Slug;
            BrandModel brand= _dataContext.Brands.Where(c => c.Slug == Slug).FirstOrDefault();
            if (brand == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var productByBrand = _dataContext.Products.Where(row => row.BrandId == brand.Id);
                return View(await productByBrand.OrderByDescending(c => c.Id).ToListAsync());
            }
        }

    }
}


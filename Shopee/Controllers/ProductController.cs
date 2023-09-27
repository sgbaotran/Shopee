using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shopee.Models;
using Shopee.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shopee.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;

        public ProductController(DataContext context)
        {
            _dataContext = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> Details(int Id )
        {
            if (Id == null) return RedirectToAction("Index");
                
            var productById = _dataContext.Products.Where(c => c.Id == Id).FirstOrDefault();

            return View(productById);
        }

        public IActionResult Error(int statusCode)
        {
            return View();
        }

    }
}


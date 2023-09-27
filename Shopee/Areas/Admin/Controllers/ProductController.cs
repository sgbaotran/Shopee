using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopee.Models;
using Shopee.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shopee.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _dataContext = context;
        }
        // GET: /<controller>/ 


        public async Task<IActionResult> Index()
        {
            //.Include("Category").Include("Brand"): This is using the Include method to specify related entities that should be loaded along with the "Products" entity.
            //that means query from Products but gonna grab those related as well
            var products = await _dataContext.Products.OrderByDescending(p => p.Id).Include(p => p.Category).Include(p => p.Brand).ToListAsync();
            return View(products);
            //return View();
        }

        /*As for this 2 methods, you will notice that the app will freeze for a while its loading because its a synchronous operation. If use the top one
         * .Users would still be able to poke around.
        */
        //public IActionResult Index()
        //{
        //    //.Include("Category").Include("Brand"): This is using the Include method to specify related entities that should be loaded along with the "Products" entity.
        //    //that means query from Products but gonna grab those related as well
        //    var products = _dataContext.Products.OrderByDescending(p => p.Id).Include(p => p.Category).Include(p => p.Brand).ToList();
        //    return View(products);
        //    //return View();
        //}



        [HttpGet]
        public IActionResult Edit(int id)
        {
            ProductModel product = _dataContext.Products.Find(id);
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);
            //return Redirect(Request.Headers["Referer"].ToString());
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name");
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name");
            //return Redirect(Request.Headers["Referer"].ToString());

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel product)
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);

            if (ModelState.IsValid)
            {
                product.Slug = product.Name.Replace(" ", "_");
                var Slug = await _dataContext.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);

                if (Slug != null)
                {
                    ModelState.AddModelError("", "Already exists");
                    return View(product);
                }

                else
                {
                    if (product.ImageUpload != null)
                    {
                        string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                        string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                        string finalPath = Path.Combine(uploadDir, imageName);


                        FileStream fs = new FileStream(finalPath, FileMode.Create);
                        await product.ImageUpload.CopyToAsync(fs);
                        fs.Close();
                        product.Image = imageName;
                    }
                }

                _dataContext.Add(product);
                _dataContext.SaveChanges();
                TempData["success"] = "All good brother";
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
            //return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductModel product)
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);

            if (ModelState.IsValid)
            {
                product.Slug = product.Name.Replace(" ", "_");
                var Slug = await _dataContext.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);

                if (Slug != null)
                {
                    ModelState.AddModelError("", "Already exists");
                    return View(product);
                }

                else
                {
                    if (product.ImageUpload != null)
                    {
                        string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                        string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                        string finalPath = Path.Combine(uploadDir, imageName);


                        FileStream fs = new FileStream(finalPath, FileMode.Create);
                        await product.ImageUpload.CopyToAsync(fs);
                        fs.Close();
                        product.Image = imageName;
                    }
                }

                _dataContext.Update(product);
                _dataContext.SaveChanges();
                TempData["success"] = "All good brother";
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

        public IActionResult Delete(int Id) {
            ProductModel product = _dataContext.Products.Find(Id);
            if (product != null)
            {
                _dataContext.Remove(product);
                _dataContext.SaveChanges();
                TempData["successs"] = "Product deleted successfully";
            }
            else {
                TempData["error"] = "Product does not exist";
                    }
            return RedirectToAction("Index");
        }
    }
}


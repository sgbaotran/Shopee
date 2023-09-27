using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopee.Models;
using Shopee.Repository;

namespace Shopee.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DataContext _dataContext;

    public HomeController(ILogger<HomeController> logger, DataContext context)
    {
        _dataContext = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        //.Include("Category").Include("Brand"): This is using the Include method to specify related entities that should be loaded along with the "Products" entity.
        //that means query from Products but gonna grab those related as well
        var products = _dataContext.Products.Include("Category").Include("Brand").ToList();
        return View(products);
        //return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


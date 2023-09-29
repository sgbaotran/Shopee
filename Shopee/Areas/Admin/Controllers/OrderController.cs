
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
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;

        public OrderController(DataContext _context)
        {
            _dataContext = _context;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var orders = await _dataContext.Orders.OrderByDescending(p => p.Id).ToListAsync();
            return View(orders);

        }

        public async Task<IActionResult> Delete(int Id)
        {
            OrderModel order = _dataContext.Orders.Find(Id);
            if (order != null)
            {
                _dataContext.Remove(order);
                _dataContext.SaveChanges();
                TempData["successs"] = "Order deleted successfully";
            }
            else
            {
                TempData["error"] = "Order does not exist";
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string orderCode)
        {
            //var orderItems = await _dataContext.OrderDetails.Include(order => order.Product).Where(order => order.OrderCode == orderCode).ToListAsync();
            var orderItems = await _dataContext.OrderDetails.Where(order => order.OrderCode == orderCode).Include(p=>p.Product).ToListAsync();

            return View(orderItems);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shopee.Models;
using Shopee.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shopee.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DataContext _dataContext;

        public CheckoutController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail == null)
            {
                RedirectToAction("Login","Account");
            }
            else
            {
                var orderCode= Guid.NewGuid().ToString();
                var orderItems = new OrderModel();
                orderItems.OrderCode = orderCode;
                orderItems.UserName = userEmail;
                orderItems.CreatedDate = DateTime.Now;
                orderItems.Status = 1;
                await _dataContext.AddAsync(orderItems);
                _dataContext.SaveChanges();

                List<CartItemModel> cart = HttpContext.Session.GetJsonCache<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

                foreach(CartItemModel product in cart)

                {
                    var newOrderDetails = new OrderDetails();
                    newOrderDetails.UserName = userEmail;
                    newOrderDetails.OrderCode = orderCode;
                    newOrderDetails.ProductId = product.ProductId;
                    newOrderDetails.Price = product.Price;
                    newOrderDetails.Quantity = product.Quantity;
                    

                    _dataContext.Add(newOrderDetails);

                    _dataContext.SaveChanges();
                }

                TempData["success"] = "Order placed successfully";
                HttpContext.Session.Remove("Cart");
                return RedirectToAction("Index","Home");
            }

            return View();
        }

 
    }
}


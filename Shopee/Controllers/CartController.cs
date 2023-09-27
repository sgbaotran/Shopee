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
    public class CartController : Controller
    {

        private readonly DataContext _dataContext;

        public CartController(DataContext context)
        {
            _dataContext = context;
        }


        public IActionResult Index()
        {
            //This line will check the Cache in user;s device to see if any List of CartItemModel there written in Json or not, else create a new one. "Cart" is the key to get the JSON
            List<CartItemModel> cartItems = HttpContext.Session.GetJsonCache<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();


            /*NOT SUPPOSED TO BE HERE FROM HERRE
            var allProds = _dataContext.Products.Include("Category").Include("Brand").ToList();

            CartItemModel macbook = new CartItemModel(allProds[0]);
            CartItemModel samsungbook = new CartItemModel(allProds[1]);

            List<CartItemModel> cartFake = new List<CartItemModel>
            {
                macbook,
                samsungbook
            };
            */

            CartModel cart = new()
            {
                CartItems = cartItems,
                /*NOT SUPPOSED TO BE HERE TO HERRE
                CartItems = cartFake,*/
                GrandTotal = cartItems.Sum(item => item.Total)
            };

            return View(cart);
        }


        public IActionResult Checkout()
        {
            return View("~/Views/Checkout/index.cshtml");
        }

        public async Task<IActionResult> Add(int Id)
        {
            ProductModel product = await _dataContext.Products.FindAsync(Id);

            List<CartItemModel> cart = HttpContext.Session.GetJsonCache<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

            CartItemModel cartItems = cart.Where(item => item.ProductId == Id).FirstOrDefault();

            if (cartItems == null)
            {
                cart.Add(new CartItemModel(product));
            }
            else
            {
                cartItems.Quantity += 1;
            }
            TempData["success"] = "Add to cart successfully!";

            HttpContext.Session.SetJsonCache("Cart", cart);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Decrease(int Id)
        {
            ProductModel product = await _dataContext.Products.FindAsync(Id);

            List<CartItemModel> cart = HttpContext.Session.GetJsonCache<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

            CartItemModel cartItem = cart.FirstOrDefault(item => item.ProductId == Id);

            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity -= 1;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == Id);
            }
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJsonCache("Cart", cart);
            }

            TempData["success"] = "Remove from cart successfully!";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Remove(int Id)
        {
            //ProductModel product = await _dataContext.Products.FindAsync(Id);

            List<CartItemModel> cart = HttpContext.Session.GetJsonCache<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

            CartItemModel cartItem = cart.FirstOrDefault(item => item.ProductId == Id);

            cart.RemoveAll(p => p.ProductId == Id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJsonCache("Cart", cart);
            }

            TempData["success"] = "Remove from cart successfully!";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Clear()
        {
            //ProductModel product = await _dataContext.Products.FindAsync(Id);

            HttpContext.Session.Remove("Cart") ;

            TempData["success"] = "Clear cart successfully!";
            return Redirect(Request.Headers["Referer"].ToString());
        }




    }
}


using System;
namespace Shopee.Models
{
    public class CartModel
    {

        public List<CartItemModel> CartItems { get; set; }


        public decimal GrandTotal { get; set; }

        public CartModel()
        {
        }
    }
}


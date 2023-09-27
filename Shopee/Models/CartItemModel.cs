using System;
namespace Shopee.Models
{
    public class CartItemModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Total
        {
            get { return Quantity * Price; }
        }

        public string Image;

        public CartItemModel(ProductModel product)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            Quantity = 1;
            Price = product.Price;
            Image = product.Image;

        }

        public CartItemModel() {
        }

    }
}


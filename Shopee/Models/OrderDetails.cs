using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopee.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string OrderCode { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public ProductModel Product;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        

        public OrderDetails()
        {
        }
    }
}


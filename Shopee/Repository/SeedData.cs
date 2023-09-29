using System;
using Microsoft.EntityFrameworkCore;
using Shopee.Models;

namespace Shopee.Repository
{
    public class SeedData
    {
        public static void SeedingData(DataContext _context)
        {
           
            //_context.Database.Migrate();


            if (!_context.Products.Any())
            {

                BrandModel Apple = new BrandModel() { Name = "Apple", Slug = "apple", Description = "Apple is the largest tech company in the world", Status = 1 };
                BrandModel Samsung = new BrandModel() { Name = "Samsung", Slug = "Samsung", Description = "Samsung is the second largest tech company in the world", Status = 2 };

                CategoryModel Macbook = new CategoryModel() { Name = "Macbook", Slug = "Macbook", Description = "Macbook is Apple's best products", Status = 1 };
                CategoryModel SamsungBook = new CategoryModel() { Name = "SamsungBook", Slug = "SamsungBook", Description = "SamsungBook is Samsung's best products", Status = 1 };

                _context.Products.AddRange(
                    new ProductModel()
                    {
                        Name = "Macboook Pro 14",
                        Slug = "Macbook",
                        Description = "This Mac has 16gb of Ram and 16 core",
                        Image = "macbook.jpeg",
                        Category = Macbook,
                        Brand = Apple,
                        Price = 2399,
                    },
                       new ProductModel()
                       {
                           Name = "Samsung Book Pro X",
                           Slug = "SamsungBook",
                           Description = "This Samsung Book has 16gb of Ram and 16 core",
                           Image = "samsung.jpeg",
                           Category = SamsungBook,
                           Brand = Samsung,
                           Price = 1799,
                       });
                _context.SaveChanges();
            }
        }
    }


}


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shopee.Repository.Validation;

namespace Shopee.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Product name must be longer than 4 characters")]
        public string Name { get; set; }

        [Required, MinLength(4, ErrorMessage = "Product description must be longer than 4 characters")]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Slug { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }

        public CategoryModel Category { get; set; }

        public BrandModel Brand { get; set; }

        public string Image { get; set; } = "";

        [NotMapped] //This means this property should not be mapped or craeted in the datbase
        //[Extension]
        [FileExtension]//This specify the validation middleware pattern is (<FileName>Attribute.cs then [FileName])
        public IFormFile ImageUpload { get; set; }

        public ProductModel()
        {
        }
    }
}


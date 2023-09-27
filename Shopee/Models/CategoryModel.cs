using System;
using System.ComponentModel.DataAnnotations;

namespace Shopee.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(4, ErrorMessage = "Category name must be longer than 4 characters")]
        public string Name { get; set; }

        [Required, MinLength(4, ErrorMessage = "Description must be longer than 4 characters")]
        public string Description { get; set; }

        public string Slug { get; set; }

        public int Status { get; set; }

        public CategoryModel()
        {
        }
    }
}


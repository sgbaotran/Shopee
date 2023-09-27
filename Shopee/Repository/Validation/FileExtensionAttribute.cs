using System;
using System.ComponentModel.DataAnnotations;

namespace Shopee.Repository.Validation
{
    public class FileExtension: ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                string[] extensions = { "jpg", "png", "jpeg" };

                bool result = extensions.Any(x => extension.EndsWith(x));

                if (!result) {
                    return new ValidationResult("Not Allowed extension ");
                }
            }
            return ValidationResult.Success;
        }
        public FileExtension()
        {
        }
    }
}


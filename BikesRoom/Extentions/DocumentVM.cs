using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
namespace BikesRoom.Extentions.DocumentVM
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           
            var file = value as IFormFile;

            var extension = Path.GetExtension(file.FileName);

            if (file != null)
            {
                if (!_extensions.Contains(extension.ToLower()) )
                {
                    return new ValidationResult(GetErrorMessage());
                }
                if (file.Length > 2097152)
                {
                    return new ValidationResult(GetErrorMessageLen());
                }
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Your Image type is not valid Allowed Only .JPG or .PNG!";
        }
        public string GetErrorMessageLen()
        {
            return $"Image size shoud be less than 2MB.";
        }
    }
 


}


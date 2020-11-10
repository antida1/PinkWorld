using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace PinkWorld.Web.Models
{
    public class SiteViewModel
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "the field {0} must contain less than {1} characteres.")]
        [Required]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "the field {0} must contain less than {1} characteres.")]
        [Required]
        public string Address { get; set; }

        [MaxLength(50, ErrorMessage = "the field {0} must contain less than {1} characteres.")]
        [Required]
        public string Phone { get; set; }

        [MaxLength(50, ErrorMessage = "the field {0} must contain less than {1} characteres.")]
        [Required]
        [Display(Name = "Bussiness Name")]
        public string BussinessName { get; set; }
        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:44357/images/no-image.png"
            : $"https://pinkworld.blob.core.windows.net/sites/{ImageId}";

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int IdCity { get; set; }

    }
}

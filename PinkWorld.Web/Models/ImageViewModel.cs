using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace PinkWorld.Web.Models
{
    public class ImageViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:44357/images/no-image.png"
            : $"https://pinkworld.blob.core.windows.net/pictures/{ImageId}";

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}

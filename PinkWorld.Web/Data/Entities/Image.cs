using System;
using System.ComponentModel.DataAnnotations;

namespace PinkWorld.Web.Data.Entities
{
    public class Image
    {
        public int Id { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        //TODO change path local
        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:44357/images/no-image.png"
            : $"https://pinkworld.blob.core.windows.net/pictures/{ImageId}";

    }
}

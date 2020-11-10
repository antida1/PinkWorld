using System;

namespace PinkWorld.Common.Models
{
    public class SiteResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string BussinessName { get; set; }
        public Guid ImageId { get; set; }
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://pinkworld.azurewebsites.net/images/no-image.png"
            : $"https://pinkworld.blob.core.windows.net/sites/{ImageId}";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int IdCity { get; set; }
        public CityResponse City { get; set; }
    }
}

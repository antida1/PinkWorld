using System;
using System.Collections.Generic;
using System.Text;

namespace PinkWorld.Common.Responses
{
    public class SiteResponse
    {
        public int Id { get; set; }


        public string Name { get; set; }


        public string Adress { get; set; }


        public string Phone { get; set; }


        public string BussinessName { get; set; }

        public Guid ImageId { get; set; }


        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:44357/images/no-image.png"
            : $"https://pinkworld.blob.core.windows.net/sites/{ImageId}";

        public double Latitude;

        public double Longitude;


        public int IdCity { get; set; }

        public CityResponse City { get; set; }
    }
}

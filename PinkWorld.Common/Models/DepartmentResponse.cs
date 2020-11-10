using System.Collections.Generic;

namespace PinkWorld.Common.Models
{
    public class DepartmentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CityResponse> Cities { get; set; }
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;
        public int IdCountry { get; set; }
        public CountryResponse Country { get; set; }
    }
}

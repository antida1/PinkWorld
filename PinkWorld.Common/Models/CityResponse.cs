using System.Collections.Generic;

namespace PinkWorld.Common.Models
{
    public class CityResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdDepartment { get; set; }
        public DepartmentResponse Department { get; set; }
        public ICollection<SiteResponse> Sites { get; set; }
        public int SitesNumber => Sites == null ? 0 : Sites.Count;
    }
}

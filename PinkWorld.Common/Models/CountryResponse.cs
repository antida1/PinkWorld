using System.Collections.Generic;

namespace PinkWorld.Common.Models
{
    public class CountryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DepartmentResponse> Departments { get; set; }
        public int DepartmentsNumber => Departments == null ? 0 : Departments.Count;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PinkWorld.Common.Entities
{
    public class CountryResponse
    {
        public int Id { get; set; }

      
        public string Name { get; set; }

        public ICollection<DepartmentResponse> Departments { get; set; }

      
        public int DepartmentsNumber => Departments == null ? 0 : Departments.Count;
    }
}

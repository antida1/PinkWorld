using System;
using System.Collections.Generic;
using System.Text;

namespace PinkWorld.Common.Responses
{
    public class CityResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public int IdDepartment { get; set; }


        public DepartmentResponse Department { get; set; }

        public ICollection<SiteResponse> Sites { get; set; }
    }
}

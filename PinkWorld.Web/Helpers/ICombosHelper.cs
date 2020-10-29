using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace PinkWorld.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboCountries();

        IEnumerable<SelectListItem> GetComboDepartments(int countryId);

        IEnumerable<SelectListItem> GetComboCities(int departmentId);

    }

}

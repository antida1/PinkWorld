using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinkWorld.Web.Data;

namespace PinkWorld.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CountriesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<IActionResult> CountriesList ()
        {
            return Ok(await _dataContext.Countries
                .Include(d=>d.Departments)
                .ThenInclude(c=>c.Cities)
                .ToListAsync());
        
        
        }

    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinkWorld.Web.Data;
using PinkWorld.Web.Data.Entities;

namespace PinkWorld.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class SitesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public SitesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }



        [HttpGet]
        public async Task<IActionResult> GetSites()
        {
            List<Site> sites = await _dataContext.Sites
                .ToListAsync();
            return Ok(sites);
        }

    }
}

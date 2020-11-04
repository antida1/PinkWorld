using PinkWorld.Web.Data;
using PinkWorld.Web.Data.Entities;
using PinkWorld.Web.Models;
using System;
using System.Threading.Tasks;

namespace PinkWorld.Web.Helpers
{
    public class SiteHelper : ISiteHelper
    {
        private readonly DataContext _context;

        public SiteHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<Site> AddSiteAsync(SiteViewModel model, Guid imageId)
        {
            Site site = new Site
            {
                Address = model.Address,
                Name = model.Name,
                ImageId = imageId,
                Phone = model.Phone,
                City = await _context.Cities.FindAsync(model.IdCity),
                BussinessName = model.BussinessName,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                IdCity = model.IdCity
            };
            return site;
        }
    }
}

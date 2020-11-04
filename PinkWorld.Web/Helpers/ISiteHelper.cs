using PinkWorld.Web.Data.Entities;
using PinkWorld.Web.Models;
using System;
using System.Threading.Tasks;

namespace PinkWorld.Web.Helpers
{
    public interface ISiteHelper
    {
        Task<Site> AddSiteAsync(SiteViewModel model, Guid imageId);
    }
}

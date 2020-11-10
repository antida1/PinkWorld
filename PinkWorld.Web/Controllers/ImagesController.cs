using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinkWorld.Web.Data;
using PinkWorld.Web.Data.Entities;
using PinkWorld.Web.Helpers;
using PinkWorld.Web.Models;
using System;
using System.Threading.Tasks;

namespace PinkWorld.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ImagesController : Controller
    {
        private readonly DataContext _context;

        private readonly IBlobHelper _blobHelper;

        public ImagesController(DataContext context, IBlobHelper blobHelper)
        {
            _context = context;
            _blobHelper = blobHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Images
                .ToListAsync());
        }

        public IActionResult Create()
        {
            return View(new ImageViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "pictures");
                }

                Image image = new Image();
                image.ImageId = imageId;
               
                try
                {
                    _context.Add(image);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }
            return View(model);
        }        

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Image image = await _context.Images
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

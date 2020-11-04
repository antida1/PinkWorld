using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinkWorld.Web.Data;
using PinkWorld.Web.Data.Entities;
using System;
using System.Threading.Tasks;

namespace PinkWorld.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuestionnairesController : Controller
    {
        private readonly DataContext _context;

        public QuestionnairesController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Questionnaires
                .ToListAsync());
        }

        public IActionResult Create()
        {
            return View(new Questionnaire());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Questionnaire questionnaire)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(questionnaire);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }                
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }
            return View(questionnaire);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Questionnaire questionnaire = await _context.Questionnaires.FindAsync(id);
            if (questionnaire == null)
            {
                return NotFound();
            }
            return View(questionnaire);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Questionnaire questionnaire)
        {
            if (id != questionnaire.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionnaire);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }                
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(questionnaire);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Questionnaire questionnaire = await _context.Questionnaires               
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (questionnaire == null)
            {
                return NotFound();
            }

            _context.Questionnaires.Remove(questionnaire);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

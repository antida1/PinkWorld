using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PinkWorld.Web.Data;

namespace PinkWorld.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionnairesController : ControllerBase
    {
        private readonly DataContext _context;

        public QuestionnairesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetQuizzes()
        {
            return Ok(_context.Questionnaires
                .ToList());
        }



    }
}

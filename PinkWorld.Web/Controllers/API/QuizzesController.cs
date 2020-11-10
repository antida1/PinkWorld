using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinkWorld.Web.Data;

namespace PinkWorld.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizzesController : ControllerBase
    {
        private readonly DataContext _context;

        public QuizzesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetQuizzes()
        {
            return Ok(_context.Quizzes
                .Include(q => q.Questions));
        }
    }
}

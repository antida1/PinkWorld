using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PinkWorld.Common.Responses;
using PinkWorld.Web.Data;
using PinkWorld.Web.Data.Entities;

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

        [HttpPut]
        public IActionResult GetQuizzes(List<QuestionnaireResponse> response)
        {
            List<Questionnaire> questionnaires = null;

            foreach (var item in response) 
            {
               Questionnaire questionnaire = _context.Questionnaires.FirstOrDefault(i=>i.Id == item.Id);

                Answer answer = new Answer
                {
                    Respond = item.Answer,
                    Question = questionnaire

                };

                questionnaires.Add(questionnaire);

                _context.Answers.Add(answer);

            }

            Quiz quiz = new Quiz
            {
                Date = DateTime.Today,
                Questions = questionnaires
            };

            _context.Quizzes.Add(quiz);

            return Ok();

        }

    }
}

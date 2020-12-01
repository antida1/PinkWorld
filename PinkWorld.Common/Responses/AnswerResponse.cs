using PinkWorld.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinkWorld.Common.Responses
{
    public class AnswerResponse
    {
        public int Id { get; set; }

      
        public string Respond { get; set; }

       
        public int QuestionId { get; set; }

       
        public QuestionnaireResponse Question { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PinkWorld.Common.Responses
{
    public class QuestionnaireResponse
    {
        public int Id { get; set; }

        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
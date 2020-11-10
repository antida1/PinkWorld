using System;
using System.Collections.Generic;
using System.Text;

namespace PinkWorld.Common.Models
{
    public class QuizResponse
    {
        public int Id { get; set; }       
        public DateTime Date { get; set; }
        public DateTime DateLocal => Date.ToLocalTime();
        public ICollection<QuestionnaireResponse> Questions { get; set; }
        public int QuestionsNumber => Questions == null ? 0 : Questions.Count;
        public int UserId { get; set; }
        public UserResponse User { get; set; }
    }
}

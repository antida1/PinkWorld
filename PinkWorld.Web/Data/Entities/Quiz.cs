using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PinkWorld.Web.Data.Entities
{
    public class Quiz
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime DateLocal => Date.ToLocalTime();

        public ICollection<Questionnaire> Questions { get; set; }

        [DisplayName("Questions Number")]
        public int QuestionsNumber => Questions == null ? 0 : Questions.Count;

        [JsonIgnore]
        [NotMapped]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }  
    }
}

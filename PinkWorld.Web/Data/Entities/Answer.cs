using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PinkWorld.Web.Data.Entities
{
    public class Answer
    {

        public int Id { get; set; }

        [MaxLength(200, ErrorMessage = "the field {0} must contain less than {1} characteres.")]
        [Required]
        public string Respond { get; set; }

        [JsonIgnore]
        [NotMapped]
        public int QuestionId { get; set; }

        [JsonIgnore]
        public Questionnaire Question { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PinkWorld.Web.Data.Entities
{
    public class Questionnaire
    {
        public int Id { get; set; }

        [MaxLength(200, ErrorMessage = "the field {0} must contain less than {1} characteres.")]
        [Required]
        public string Question { get; set; }
    }
}
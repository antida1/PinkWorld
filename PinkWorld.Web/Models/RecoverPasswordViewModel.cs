using System.ComponentModel.DataAnnotations;

namespace PinkWorld.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}

using System.ComponentModel.DataAnnotations;

namespace PinkWorld.Common.Request
{
    public class EmailRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PinkWorld.Common.Request
{
    public class UserRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }


        [MaxLength(20)]
        [Required]
        public string Document { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50)]
        [Required]
        public string SecondName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public int CityId { get; set; }


        public byte[] ImageArray { get; set; }


        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }


        public string PasswordConfirm { get; set; }


    }
}

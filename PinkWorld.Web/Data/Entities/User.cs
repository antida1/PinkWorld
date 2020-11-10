﻿using Microsoft.AspNetCore.Identity;
using PinkWorld.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinkWorld.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(20)]
        [Required]
        public string Document { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Second Name")]
        [MaxLength(50)]
        [Required]
        public string SecondName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

       //TODO change path local
        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://pinkworld.azurewebsites.net/images/no-image.png"
            : $"https://pinkworld.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        public City City { get; set; }

        [Display(Name = "User")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "User")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

    }
}

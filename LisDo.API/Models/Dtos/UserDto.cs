using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LisDo.API.Models.Dtos
{
    public class UserDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Remote(action: "IsNameInUse", controller: "Account")]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string Email { get; set; }
    }
}

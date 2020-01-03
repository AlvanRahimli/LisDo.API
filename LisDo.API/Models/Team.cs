using LisDo.API.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LisDo.API.Models
{
    public class Team : EntityBase
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Invalid name entry")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Invalid desc entry")]
        public string Description { get; set; }

        [Required]
        [ValidType(allowedType: "public,private")]
        public string Type { get; set; }

        public int LisdoCount { get; set; }

        public int MemberCount { get; set; }

        public ICollection<Team_User> Team_Users { get; set; }

        public ICollection<Team_Admin> Team_Admins { get; set; }

        public ICollection<Lisdo> Lisdos { get; set; }
    }
}

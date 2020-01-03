using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LisDo.API.Models
{
    public class User : IdentityUser
    {
        public ICollection<Lisdo> Lisdos { get; set; }
        public ICollection<Team_User> Team_Users { get; set; }
        public ICollection<Item_User> Item_Users { get; set; }
        public ICollection<Team_Admin> Team_Admins { get; set; }
    }
}

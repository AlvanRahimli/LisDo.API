using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LisDo.API.Models
{
    public class Team_User
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}

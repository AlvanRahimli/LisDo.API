using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LisDo.API.Models
{
    public class Team_Admin
    {
        public string AdminId { get; set; }
        public User Admin { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}

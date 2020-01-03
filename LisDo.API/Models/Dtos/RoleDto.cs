using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LisDo.API.Models.Dtos
{
    public class RoleDto
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}

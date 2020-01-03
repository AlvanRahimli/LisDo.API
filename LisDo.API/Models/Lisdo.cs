using LisDo.API.Utilities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LisDo.API.Models
{
    public class Lisdo : EntityBase
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        [ValidType(allowedType: "public,private,team")]
        public string Type { get; set; }
       
        public int UpvoteCount { get; set; }

        [Range(0, 5)]
        public int Priority { get; set; }

        public int TeamId { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public Team Team { get; set; }

        public User Author { get; set; }

        public ICollection<LisdoItem> Items { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LisDo.API.Models
{
    public class LisdoItem : EntityBase
    {
        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string Content { get; set; }

        [Range(0, 1000)]
        public int RequiredClick { get; set; }

        [Range(0, 1000)]
        public int Clicked { get; set; }

        [Range(0, 100)]
        public int DonePercentage
        {
            get
            {
                return Clicked * 100 / RequiredClick;
            }
        }

        [Required]
        public int LisdoId { get; set; }

        [Required]
        public int Order { get; set; }

        public Lisdo Lisdo { get; set; }

        public ICollection<Item_User> Item_Users { get; set; }
    }
}

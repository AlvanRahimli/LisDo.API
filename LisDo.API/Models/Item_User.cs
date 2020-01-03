using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LisDo.API.Models
{
    public class Item_User
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int LisdoItemId { get; set; }
        public LisdoItem LisdoItem { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LisDo.API.Models.Dtos
{
    public class ItemCreateDto
    {
        public string Content { get; set; }
        public int RequiredClick { get; set; }
        public int LisdoId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LisDo.API.Models.Dtos
{
    public class LisdoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int ItemCount { get; set; }
        public int AverageDone { get; set; }
        public int Priority { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int UpvoteCount { get; set; }
    }
}

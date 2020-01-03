using System;

namespace LisDo.API.Models.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int RequiredClick { get; set; }
        public int Clicked { get; set; }
        public int DonePercentage { get; set; }
        public int LisdoId { get; set; }
        public int Order { get; set; }        
    }
}

using LisDo.API.Models;
using LisDo.API.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LisDo.API.Repositories.ListItems
{
    public interface ILisdoItemsRepo
    {
        Task<ICollection<ItemDto>> SearchItem(string searchTerm, string uId, int lisdoId);
        Task<bool> AddItems(List<ItemCreateDto> lisdoItems, string uId);
        Task<bool> UpdateItem(ItemDto item, string uId);
        Task<bool> DeleteItem(int itemId, string uId);
        Task<int> DoItem(int itemId, string uId);
        Task<int> UndoItem(int itemId, string uId);
    }
}

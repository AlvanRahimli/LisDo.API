using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LisDo.API.Data;
using LisDo.API.Models;
using LisDo.API.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace LisDo.API.Repositories.ListItems
{
    public class LisdoItemsRepo : ILisdoItemsRepo
    {
        private readonly LisDoDbContext _context;

        public LisdoItemsRepo(LisDoDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> AddItems(List<ItemCreateDto> lisdoItems, string uId)
        {
            var newItems = new List<LisdoItem>();

            foreach(var item in lisdoItems)
            {
                newItems.Add(new LisdoItem
                {
                    Content = item.Content,
                    RequiredClick = item.RequiredClick,
                    Clicked = 0,
                    LisdoId = item.LisdoId,
                    Order = lisdoItems.IndexOf(item),
                    IsNew = false
                });
            }

            await _context.LisdoItems.AddRangeAsync(newItems);

            var res = await _context.SaveChangesAsync();

            if (res < 1)
                return false;
            return true;
        }

        public async Task<bool> UpdateItem(ItemDto item, string uId)
        {
            var oldItem = await _context.LisdoItems
                .FirstOrDefaultAsync(i => i.Id == item.Id);

            if (oldItem == null)
                return false;

            var lisdo = await _context.Lisdos
                .FirstOrDefaultAsync(l => l.Id == item.LisdoId);

            if (lisdo.Type == "public" || lisdo.Type == "private")
            {
                if (lisdo.AuthorId == uId)
                {
                    oldItem.Content = item.Content;
                    oldItem.RequiredClick = item.RequiredClick;
                    oldItem.Order = item.Order;

                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            else if (lisdo.Type == "team")
            {
                var isAdmin = await _context.Team_Admins
                    .AnyAsync(ta => ta.AdminId == uId && ta.TeamId == lisdo.TeamId);

                if (isAdmin)
                {
                    oldItem.Content = item.Content;
                    oldItem.RequiredClick = item.RequiredClick;
                    oldItem.Order = item.Order;

                    await _context.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }

        public async Task<int> DoItem(int itemId, string uId)
        {
            var item = await _context.LisdoItems
                .FirstOrDefaultAsync(i => i.Id == itemId);

            if (item.Clicked >= item.RequiredClick)
                return item.RequiredClick;

            item.Clicked += 1;

            await _context.SaveChangesAsync();            

            return item.Clicked;
        }

        public async Task<int> UndoItem(int itemId, string uId)
        {
            var item = await _context.LisdoItems
                .FirstOrDefaultAsync(i => i.Id == itemId);

            if (item.Clicked == 0)
                return 0;

            item.Clicked -= 1;

            await _context.SaveChangesAsync();

            return item.Clicked;
        }

        public async Task<bool> DeleteItem(int itemId, string uId)
        {
            var item = await _context.LisdoItems
                .FirstOrDefaultAsync(i => i.Id == itemId);

            var lisdo = await _context.Lisdos.FirstOrDefaultAsync(l => l.Items.Any(i => i.Id == itemId));

            if(lisdo.Type == "public" || lisdo.Type == "private")
            {
                if (lisdo.AuthorId == uId)
                    _context.LisdoItems.Remove(item);
            }
            else if(lisdo.Type == "team")
            {
                var IsAdmin = await _context.Team_Admins
                    .AnyAsync(tu => tu.AdminId == uId && tu.TeamId == lisdo.TeamId);
                if (IsAdmin) 
                {
                    _context.LisdoItems.Remove(item);
                }
            }

            var res = await _context.SaveChangesAsync();
            return res < 1 ? false : true;
        }

        public async Task<ICollection<ItemDto>> SearchItem(string searchTerm, string uId, int lisdoId)
        {
            var items = await (from item in _context.LisdoItems
                               join lisdo in _context.Lisdos
                               on item.LisdoId equals lisdo.Id
                               where lisdo.Id == lisdoId
                               select new ItemDto
                               {
                                   Id = item.Id,
                                   Content = item.Content,
                                   RequiredClick = item.RequiredClick,
                                   Clicked = item.Clicked,
                                   DonePercentage = item.DonePercentage,
                                   LisdoId = lisdo.Id,
                                   Order = item.Order
                               }).ToListAsync();

            var lis = await _context.Lisdos.FirstOrDefaultAsync(l => l.Id == lisdoId);

            if (lis.Type == "private")
            {
                if (lis.AuthorId == uId)
                    return items;
            }
            else if (lis.Type == "team")
            {
                if (await _context.Team_Users.AnyAsync(tu => tu.UserId == uId && tu.TeamId == lis.TeamId))
                    return items;
            }
            else if (lis.Type == "public")
                return items;
            
            return null;
        }
    }
}

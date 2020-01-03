using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LisDo.API.Data;
using LisDo.API.Models;
using LisDo.API.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace LisDo.API.Repositories.Lisdos
{
    //READY
    public class LisdosRepo : ILisdosRepo
    {
        private readonly LisDoDbContext _context;

        public LisdosRepo(LisDoDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<LisdoDto>> GetLisdosFeed(int rn, int c)
        {
            var result = await (from lisdo in _context.Lisdos
                                join author in _context.Users
                                on lisdo.AuthorId equals author.Id
                                join team in _context.Teams
                                on lisdo.TeamId equals team.Id
                                where lisdo.Type == "public"
                                orderby lisdo.UpvoteCount descending
                                select new LisdoDto
                                {
                                    Id = lisdo.Id,
                                    AuthorId = author.Id,
                                    TeamId = team.Id,
                                    Name = lisdo.Name,
                                    AuthorName = author.UserName,
                                    TeamName = team.Name,
                                    Desc = lisdo.Description,
                                    Priority = lisdo.Priority,
                                    ItemCount = lisdo.Items.Count,
                                    UpvoteCount = lisdo.UpvoteCount,
                                    AverageDone = CalcAverageDone(lisdo.Items)
                                }).Skip((rn - 1) * c).Take(c).ToListAsync();

            return result;
        }

        public async Task<ICollection<LisdoDto>> GetProfileLisdos(int rn, int c, string uId)
        {
            var result = await (from lisdo in _context.Lisdos
                                join author in _context.Users
                                on lisdo.AuthorId equals author.Id
                                join team in _context.Teams
                                on lisdo.TeamId equals team.Id
                                where lisdo.AuthorId == uId
                                orderby lisdo.Priority descending
                                select new LisdoDto
                                {
                                    Id = lisdo.Id,
                                    AuthorId = uId,
                                    TeamId = team.Id,
                                    AuthorName = author.UserName,
                                    Desc = lisdo.Description,
                                    Name = lisdo.Name,
                                    TeamName = team.Name,
                                    Priority = lisdo.Priority,
                                    ItemCount = lisdo.Items.Count,
                                    UpvoteCount = lisdo.UpvoteCount,
                                    AverageDone = CalcAverageDone(lisdo.Items)
                                }).Skip((rn - 1) * c).Take(c).ToListAsync();

            return result;
        }

        public async Task<ICollection<LisdoDto>> GetTeamLisdos(int rn, int c, int tId, string uId)
        {
            var result = await (from lisdo in _context.Lisdos
                                join author in _context.Users
                                on lisdo.AuthorId equals author.Id
                                join team in _context.Teams
                                on lisdo.TeamId equals team.Id
                                where lisdo.Type == "team"
                                && lisdo.TeamId == tId
                                && _context.Team_Users
                                .Any(t => t.TeamId == tId && t.UserId == uId)
                                orderby lisdo.UpvoteCount descending
                                select new LisdoDto
                                {
                                    Id = lisdo.Id,
                                    Name = lisdo.Name,
                                    Desc = lisdo.Description,
                                    Priority = lisdo.Priority,
                                    UpvoteCount = lisdo.UpvoteCount,
                                    ItemCount = lisdo.Items.Count,
                                    AverageDone = CalcAverageDone(lisdo.Items),
                                    AuthorId = author.Id,
                                    AuthorName = author.UserName,
                                    TeamId = team.Id,
                                    TeamName = team.Name
                                }).Skip((rn - 1) * c).Take(c).ToListAsync();

            return result;
        }

        public async Task<Lisdo> GetLisdo(int id, string uId)
        {
            var result = await (from lisdo in _context.Lisdos
                                join author in _context.Users
                                on lisdo.AuthorId equals author.Id
                                where lisdo.Id == id
                                select new Lisdo
                                {
                                    Id = lisdo.Id,
                                    AuthorId = lisdo.AuthorId,
                                    Description = lisdo.Description,
                                    Items = lisdo.Items,
                                    Name = lisdo.Name,
                                    Priority = lisdo.Priority,
                                    TeamId = lisdo.TeamId,
                                    Type = lisdo.Type,
                                    UpvoteCount = lisdo.UpvoteCount,
                                    AuthorName = author.UserName
                                }).SingleOrDefaultAsync();
            if(result != null)
            {
                if (result.Type == "private")
                {
                    if (result.AuthorId == uId)
                        return result;
                }
                else if (result.Type == "team")
                {
                    var res = await _context.Team_Users
                        .AnyAsync(tu => tu.TeamId == result.TeamId && tu.UserId == result.AuthorId);
                    if (res)
                        return result;
                }
                else if (result.Type == "public")
                    return result;
            }

            return null;
        }

        public async Task<Lisdo> GetLisdoGuest(int id)
        {
            var result = await (from lisdo in _context.Lisdos
                                join author in _context.Users
                                on lisdo.AuthorId equals author.Id
                                where lisdo.Id == id 
                                && lisdo.Type == "public"
                                select new Lisdo
                                {
                                    Id = lisdo.Id,
                                    AuthorId = lisdo.AuthorId,
                                    Description = lisdo.Description,
                                    Items = lisdo.Items,
                                    Name = lisdo.Name,
                                    Priority = lisdo.Priority,
                                    TeamId = lisdo.TeamId,
                                    Type = lisdo.Type,
                                    UpvoteCount = lisdo.UpvoteCount,
                                    AuthorName = author.UserName
                                }).SingleOrDefaultAsync();
            return result;
        }

        public async Task<ICollection<LisdoDto>> SearchLisdo(string searchTerm, string type, bool lookItems, string uId, int tId, int rn)
        {
            if (!lookItems)
            {
                if (type == "public")
                {
                    var lisdos = await (from lisdo in _context.Lisdos
                                        where lisdo.Type == "public"
                                        && (lisdo.Name.Contains(searchTerm)
                                        || lisdo.Description.Contains(searchTerm))
                                        orderby lisdo.UpvoteCount descending
                                        select new LisdoDto
                                        {
                                            Id = lisdo.Id,
                                            Name = lisdo.Name,
                                            Desc = lisdo.Description,
                                        }).Skip(rn * 20).Take(20).ToListAsync();

                    return lisdos;
                }
                else if (type == "private")
                {
                    var lisdos = await (from lisdo in _context.Lisdos
                                        where lisdo.Type == "private"
                                        && (lisdo.Name.Contains(searchTerm)
                                        || lisdo.Description.Contains(searchTerm))
                                        && lisdo.AuthorId == uId
                                        orderby lisdo.UpvoteCount descending
                                        select new LisdoDto
                                        {
                                            Id = lisdo.Id,
                                            Name = lisdo.Name,
                                            Desc = lisdo.Description,
                                        }).Skip(rn * 20).Take(20).ToListAsync();

                    return lisdos;
                }
                else if (type == "team")
                {
                    var lisdos = await (from lisdo in _context.Lisdos
                                        where lisdo.Type == "team"
                                        && (lisdo.Name.Contains(searchTerm)
                                        || lisdo.Description.Contains(searchTerm))
                                        && lisdo.TeamId == tId
                                        orderby lisdo.UpvoteCount descending
                                        select new LisdoDto
                                        {
                                            Id = lisdo.Id,
                                            Name = lisdo.Name,
                                            Desc = lisdo.Description,
                                        }).Skip(rn * 20).Take(20).ToListAsync();

                    return lisdos;
                }
                else
                    return null;
            }
            else
            {
                if (type == "public")
                {
                    var lisdos = await _context.Lisdos
                        .Include(l => l.Items)
                        .Where(l => l.Type == "public"
                        && (l.Name.Contains(searchTerm)
                        || l.Description.Contains(searchTerm)
                        || l.Items.Any(i => i.Content.Contains(searchTerm))))
                        .Skip((rn - 1) * 20)
                        .Take(20)
                        .ToListAsync();

                    List<LisdoDto> lisdoDtos = new List<LisdoDto>();

                    foreach (var lisdo in lisdos)
                    {
                        lisdoDtos.Add(new LisdoDto
                        {
                            Id = lisdo.Id,
                            Name = lisdo.Name,
                            Desc = lisdo.Description
                        });
                    }

                    return lisdoDtos;
                }
                else if (type == "private")
                {
                    var lisdos = await _context.Lisdos
                        .Include(l => l.Items)
                        .Where(l => l.Type == "private"
                        && l.AuthorId == uId
                        && (l.Name.Contains(searchTerm)
                        || l.Description.Contains(searchTerm)
                        || l.Items.Any(i => i.Content.Contains(searchTerm))))
                        .Skip((rn - 1) * 20)
                        .Take(20)
                        .ToListAsync();

                    List<LisdoDto> lisdoDtos = new List<LisdoDto>();

                    foreach (var lisdo in lisdos)
                    {
                        lisdoDtos.Add(new LisdoDto
                        {
                            Id = lisdo.Id,
                            Name = lisdo.Name,
                            Desc = lisdo.Description
                        });
                    }

                    return lisdoDtos;
                }
                else if (type == "team")
                {
                    var lisdos = await _context.Lisdos
                        .Include(l => l.Items)
                        .Where(l => l.Type == "team"
                        && l.AuthorId == uId
                        && l.TeamId == tId
                        && (l.Name.Contains(searchTerm)
                        || l.Description.Contains(searchTerm)
                        || l.Items.Any(i => i.Content.Contains(searchTerm))))
                        .Skip((rn - 1) * 20)
                        .Take(20)
                        .ToListAsync();

                    List<LisdoDto> lisdoDtos = new List<LisdoDto>();

                    foreach (var lisdo in lisdos)
                    {
                        lisdoDtos.Add(new LisdoDto
                        {
                            Id = lisdo.Id,
                            Name = lisdo.Name,
                            Desc = lisdo.Description
                        });
                    }

                    return lisdoDtos;
                }
                else
                    return null;
            }
        }

        public async Task<bool> UpdateLisdo(Lisdo lisdo, string uId)
        {
            if(lisdo.IsNew)
            {
                _context.Lisdos.Add(lisdo);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                var ltu = await (from ld in _context.Lisdos
                                 where ld.Id == lisdo.Id
                                 select new Lisdo
                                 {
                                     Id = ld.Id,
                                     AuthorId = ld.AuthorId,
                                     Description = ld.Description,
                                     Items = ld.Items,
                                     Name = ld.Name,
                                     Priority = ld.Priority,
                                     TeamId = ld.TeamId,
                                     Type = ld.Type,
                                     UpvoteCount = ld.UpvoteCount,
                                     AuthorName = ld.AuthorName
                                 }).SingleOrDefaultAsync();
                
                if(ltu.AuthorId == uId)
                {
                    ltu.Name = lisdo.Name;
                    ltu.Description = lisdo.Description;
                    ltu.Type = lisdo.Type;                
                    ltu.Priority = lisdo.Priority;
                    await _context.SaveChangesAsync();
                    return true;
                }              
            }

            return false;
        }

        public async Task<bool> Delete(int id, string uId)
        {
            var ltd = await (from lisdo in _context.Lisdos
                             join author in _context.Users
                             on lisdo.AuthorId equals author.Id
                             where lisdo.Id == id
                             select new Lisdo
                             {
                                 Id = lisdo.Id,
                                 AuthorId = lisdo.AuthorId,
                                 Description = lisdo.Description,
                                 Items = lisdo.Items,
                                 Name = lisdo.Name,
                                 Priority = lisdo.Priority,
                                 TeamId = lisdo.TeamId,
                                 Type = lisdo.Type,
                                 UpvoteCount = lisdo.UpvoteCount
                             }).SingleOrDefaultAsync();

            if(ltd.Type == "private")
            {
                if(ltd.AuthorId == uId)
                {
                    _context.Lisdos.Remove(ltd);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            else if (ltd.Type == "team")
            {
                var res = await _context.Team_Admins
                    .AnyAsync(ta => ta.AdminId == uId && ta.TeamId == ltd.TeamId);
                if (res)
                {
                    _context.Lisdos.Remove(ltd);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            else if (ltd.Type == "public")
            {
                if(ltd.AuthorId == uId)
                {
                    _context.Lisdos.Remove(ltd);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }

        private int CalcAverageDone(ICollection<LisdoItem> source)
        {
            int sum = 0;
            foreach (var item in source)
            {
                sum += (item.Clicked / item.RequiredClick) * 100;
            }

            return sum / source.Count;
        }
    }
}

using LisDo.API.Models;
using LisDo.API.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LisDo.API.Repositories.Lisdos
{
    public interface ILisdosRepo
    {
        Task<ICollection<LisdoDto>> GetLisdosFeed(int rn, int c);
        Task<ICollection<LisdoDto>> GetProfileLisdos(int rn, int c, string uId);
        Task<ICollection<LisdoDto>> GetTeamLisdos(int rn, int c, int tId, string uId);
        Task<ICollection<LisdoDto>> SearchLisdo(string searchTerm, string type, bool lookItems, string uId, int tId, int rn);
        Task<Lisdo> GetLisdo(int id, string uId);
        Task<Lisdo> GetLisdoGuest(int id);
        Task<bool> UpdateLisdo(Lisdo lisdo, string uId);
        Task<bool> Delete(int id, string uId);        
    }
}

using LisDo.API.Models;
using LisDo.API.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LisDo.API.Repositories.Teams
{
    public interface ITeamsRepo
    {
        Task<ICollection<TeamDto>> GetJoinedTeams(int userId, int rn, int c);
        Task<ICollection<TeamDto>> GetPublicTeams(int rn, int c);
        Task<Team> GetTeam(int teamId);
        Task<bool> UpdateTeam(Team team);
        Task<bool> DeleteTeam(int teamId);
        Task<bool> SetAdmin(int teamId, int userId);
        Task<bool> RemoveAdmin(int teamId, int userId);
        Task<bool> AddMember(int teamId, int userId);
        Task<bool> RemoveMember(int teamId, int userId);
    }
}

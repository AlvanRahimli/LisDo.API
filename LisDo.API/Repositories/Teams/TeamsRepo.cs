using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LisDo.API.Models;
using LisDo.API.Models.Dtos;

namespace LisDo.API.Repositories.Teams
{
    public class TeamsRepo : ITeamsRepo
    {
        public Task<bool> AddMember(int teamId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTeam(int teamId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<TeamDto>> GetJoinedTeams(int userId, int rn, int c)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<TeamDto>> GetPublicTeams(int rn, int c)
        {
            throw new NotImplementedException();
        }

        public Task<Team> GetTeam(int teamId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAdmin(int teamId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveMember(int teamId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAdmin(int teamId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTeam(Team team)
        {
            throw new NotImplementedException();
        }
    }
}

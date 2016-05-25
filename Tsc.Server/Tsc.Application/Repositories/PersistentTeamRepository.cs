using System;
using System.Collections.Generic;
using Tsc.DataAccess;
using Tsc.Application.Definition;
using Tsc.Domain;

namespace Tsc.Application.Repositories
{
    public class PersistentTeamRepository : ITeamRepository
    {
        private readonly ITscDataAccess _dataAccess;

        public PersistentTeamRepository(ITscDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public void Save(Team team)
        {
            _dataAccess.Save(team);
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _dataAccess.GetAllTeams();
        }

        public Team GetTeam(Guid id)
        {
            return _dataAccess.GetTeam(id);
        }
    }
}
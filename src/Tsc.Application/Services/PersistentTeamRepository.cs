using System.Collections.Generic;
using Tsc.DataAccess;
using Tsc.Domain;
using Tsc.Domain.ExternalServices;

namespace Tsc.Application.Services
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Tsc.Domain;
using Tsc.Domain.ExternalServices;

namespace Tsc.Application.Services
{
    public class TeamRepository : ITeamRepository
    {
        // ReSharper disable once InconsistentNaming
        private static readonly List<Team> _teams =
            new List<Team>
            {
                new Team("Kis János csapat", new List<User>()),
                new Team("Matek-tenger kalózai", new List<User>()),
                new Team("Nyerünk az fix", new List<User>()),
                new Team("Szappanbuborékok", new List<User>()),
                new Team("Csacsicsapat", new List<User>()),
                new Team("Reményteli esetek", new List<User>()),
                new Team("Hullámreccsentők", new List<User>()),
                new Team("Fakanálforgatók", new List<User>())
            };

        public void Save(Team team)
        {
            _teams.Add(team);
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _teams;
        }

        public Team GetTeam(Guid id)
        {
            return _teams.First(item => item.Id == id);
        }
    }
}

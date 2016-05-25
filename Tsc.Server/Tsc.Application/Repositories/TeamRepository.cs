using System;
using System.Collections.Generic;
using System.Linq;
using Tsc.Domain;
using Tsc.Application.Definition;

namespace Tsc.Application.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        // ReSharper disable once InconsistentNaming
        private static readonly List<Team> _teams =
            new List<Team>
            {
                new Team("Kis János csapat", new List<Player>(), string.Empty),
                new Team("Matek-tenger kalózai", new List<Player>(), string.Empty),
                new Team("Nyerünk az fix", new List<Player>(), string.Empty),
                new Team("Szappanbuborékok", new List<Player>(), string.Empty),
                new Team("Csacsicsapat", new List<Player>(), string.Empty),
                new Team("Reményteli esetek", new List<Player>(), string.Empty),
                new Team("Hullámreccsentők", new List<Player>(), string.Empty),
                new Team("Fakanálforgatók", new List<Player>(), string.Empty)
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

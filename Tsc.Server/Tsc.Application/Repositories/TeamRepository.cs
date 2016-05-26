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
                new Team("Fakanálforgatók", new List<Player>(), string.Empty),
                new Team("Csapat ", new List<Player>(), string.Empty),
                new Team("Csapat 1", new List<Player>(), string.Empty),
                new Team("Csapat 2", new List<Player>(), string.Empty),
                new Team("Csapat 3", new List<Player>(), string.Empty),
                new Team("Csapat 4", new List<Player>(), string.Empty),
                new Team("Csapat 5", new List<Player>(), string.Empty),
                new Team("Csapat 6", new List<Player>(), string.Empty),
                new Team("Csapat 7", new List<Player>(), string.Empty),
                new Team("Csapat 8", new List<Player>(), string.Empty),
                new Team("Csapat 9", new List<Player>(), string.Empty),
                new Team("Csapat 10", new List<Player>(), string.Empty),
                new Team("Csapat 11", new List<Player>(), string.Empty),
                new Team("Csapat 12", new List<Player>(), string.Empty),
                new Team("Csapat 13", new List<Player>(), string.Empty),
                new Team("Csapat 14", new List<Player>(), string.Empty),
                new Team("Csapat 15", new List<Player>(), string.Empty),
                new Team("Csapat 16", new List<Player>(), string.Empty),
                new Team("Csapat 17", new List<Player>(), string.Empty),
                new Team("Csapat 18", new List<Player>(), string.Empty),
                new Team("Csapat 19", new List<Player>(), string.Empty),
                new Team("Csapat 20", new List<Player>(), string.Empty),
                new Team("Csapat 21", new List<Player>(), string.Empty),
                new Team("Csapat 22", new List<Player>(), string.Empty),
                new Team("Csapat 23", new List<Player>(), string.Empty),
                new Team("Csapat 24", new List<Player>(), string.Empty),
                new Team("Csapat 25", new List<Player>(), string.Empty),
                new Team("Csapat 26", new List<Player>(), string.Empty),
                new Team("Csapat 27", new List<Player>(), string.Empty),
                new Team("Csapat 28", new List<Player>(), string.Empty),
                new Team("Csapat 29", new List<Player>(), string.Empty),
                new Team("Csapat 30", new List<Player>(), string.Empty),
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

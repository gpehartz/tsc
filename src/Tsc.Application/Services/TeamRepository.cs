using System.Collections.Generic;
using Tsc.Domain;
using Tsc.Domain.ExternalServices;

namespace Tsc.Application.Services
{
    public class TeamRepository : ITeamRepository
    {
        private static List<Team> _teams = new List<Team>
            {
                new Team("Kis János csapat"),
                new Team("Matek-tenger kalózai"),
                new Team("Nyerünk az fix"),
                new Team("Szappanbuborékok"),
                new Team("Csacsicsapat"),
                new Team("Reményteli esetek"),
                new Team("Hullámreccsentők"),
                new Team("Fakanálforgatók")
            };

        public void Save(Team team)
        {
            _teams.Add(team);
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _teams;
        }
    }
}

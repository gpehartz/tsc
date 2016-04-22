using System.Collections.Generic;
using Tsc.Domain;
using Tsc.Domain.ExternalServices;

namespace Tsc.Application.Services
{
    public class TeamRepository : ITeamRepository
    {
        public List<Team> GetAllTeams()
        {
            return new List<Team>
            {
                new Team {Id=1, Name="Kis János csapat"},
                new Team {Id=2, Name="Matek-tenger kalózai"},
                new Team {Id=3, Name="Nyerünk az fix"},
                new Team {Id=4, Name="Szappanbuborékok"},
                new Team {Id=5, Name="Csacsicsapat"},
                new Team {Id=6, Name="Reményteli esetek"},
                new Team {Id=7, Name="Hullámreccsentők"},
                new Team {Id=8, Name="Fakanálforgatók"},
            };
        }
    }
}

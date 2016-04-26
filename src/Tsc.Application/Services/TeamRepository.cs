using System;
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
                new Team(Guid.NewGuid(), "Kis János csapat"),
                new Team(Guid.NewGuid(), "Matek-tenger kalózai"),
                new Team(Guid.NewGuid(), "Nyerünk az fix"),
                new Team(Guid.NewGuid(), "Szappanbuborékok"),
                new Team(Guid.NewGuid(), "Csacsicsapat"),
                new Team(Guid.NewGuid(), "Reményteli esetek"),
                new Team(Guid.NewGuid(), "Hullámreccsentők"),
                new Team(Guid.NewGuid(), "Fakanálforgatók")
            };
        }
    }
}

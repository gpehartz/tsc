using System;
using System.Collections.Generic;

namespace Tsc.Domain.ExternalServices
{
    public interface ITeamRepository
    {
        void Save(Team team);
        IEnumerable<Team> GetAllTeams();
        Team GetTeam(Guid id);
    }
}

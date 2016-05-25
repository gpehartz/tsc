using System;
using System.Collections.Generic;
using Tsc.Domain;

namespace Tsc.Application.Definition
{
    public interface ITeamRepository
    {
        void Save(Team team);
        IEnumerable<Team> GetAllTeams();
        Team GetTeam(Guid id);
    }
}

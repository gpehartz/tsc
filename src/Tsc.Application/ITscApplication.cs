using System;
using System.Collections.Generic;
using Tsc.Application.ServiceModel;

namespace Tsc.Application
{
    public interface ITscApplication
    {
        void AddTeam(Team team);

        void AddTournament(Tournament tournament);

        IEnumerable<Tournament> GetAllTournaments();

        Tournament GetTournament(Guid id);

        IEnumerable<Team> GetAllTeams();
    }
}
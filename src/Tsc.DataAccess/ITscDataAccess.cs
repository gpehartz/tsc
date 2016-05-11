using System;
using System.Collections.Generic;
using Tsc.Domain;

namespace Tsc.DataAccess
{
    public interface ITscDataAccess
    {
        void Save(Team team);
        IEnumerable<Team> GetAllTeams();
        void Save(Tournament tournament);
        IEnumerable<Tournament> GetAllTournaments();
        Tournament GetTournament(Guid id);
        Team GetTeam(Guid id);
    }
}
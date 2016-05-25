using System;
using System.Collections.Generic;
using Tsc.Domain;

namespace Tsc.Application
{
    public interface ITscApplication
    {
        IEnumerable<Team> GetAllTeams();

        Team GetTeam(Guid id);

        Team AddTeam(Team team);
        
        IEnumerable<Tournament> GetAllTournaments();
        
        IEnumerable<Player> GetAllPlayers();

        Tournament GetTournament(Guid id);

        Tournament AddTournament(Tournament tournament);

        void SetFixtureResult(Guid tournamentId, Guid fixtureId, IEnumerable<MatchResult> results);
    }
}
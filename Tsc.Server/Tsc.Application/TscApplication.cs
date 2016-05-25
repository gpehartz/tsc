using System;
using System.Collections.Generic;
using Tsc.Application.Definition;
using Tsc.Domain;

namespace Tsc.Application
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class TscApplication : ITscApplication
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITournamentRepository _tournamentRepository;
        
        public TscApplication(IPlayerRepository playerRepository, ITeamRepository teamRepository, ITournamentRepository tournamentRepository)
        {
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
            _tournamentRepository = tournamentRepository;
        }

        #region Team related 

        public IEnumerable<Team> GetAllTeams()
        {
            return _teamRepository.GetAllTeams();
        }

        public Team GetTeam(Guid id)
        {
            return _teamRepository.GetTeam(id);
        }

        public Team AddTeam(Team team)
        {
            _teamRepository.Save(team);
            return team;
        }

        #endregion Team related 

        #region Tournament related 

        public IEnumerable<Tournament> GetAllTournaments()
        {
            return _tournamentRepository.GetAllTournaments();
        }

        public Tournament GetTournament(Guid id)
        {
            return _tournamentRepository.GetTournament(id);
        }

        public Tournament AddTournament(Tournament tournament)
        {
            _tournamentRepository.Save(tournament);
            return tournament;
        }

        public void SetFixtureResult(Guid tournamentId, Guid fixtureId, IEnumerable<MatchResult> results)
        {
            var tournament = _tournamentRepository.GetTournament(tournamentId);
            tournament.SetFixtureResult(fixtureId, results);
            _tournamentRepository.Save(tournament);
        }

        #endregion Tournament related 

        #region Player related 

        public IEnumerable<Player> GetAllPlayers()
        {
            return _playerRepository.GetAllPlayers();
        }

        #endregion Player related 
    }
}

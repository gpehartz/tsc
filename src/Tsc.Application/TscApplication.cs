using System;
using System.Collections.Generic;
using System.Linq;
using Tsc.Application.ServiceModel;
using Tsc.Domain.ExternalServices;

namespace Tsc.Application
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class TscApplication : ITscApplication
    {
        private ITranslator _translator;
        private IUserRepository _userRepository;
        private ITeamRepository _teamRepository;
        private ITournamentRepository _tournamentRepository;
        private IFileRepository _fileRepository;
        
        public TscApplication(IUserRepository userRepository, ITeamRepository teamRepository, ITournamentRepository tournamentRepository, ITranslator translator)
        {
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _tournamentRepository = tournamentRepository;
            _translator = translator;
        }

        public IEnumerable<Team> GetAllTeams()
        {
            var domainTeams = _teamRepository.GetAllTeams();
            return domainTeams.Select(_translator.TranslateToService).ToList();
        }

        public Team GetTeam(Guid id)
        {
            var domainTeam = _teamRepository.GetTeam(id);
            return _translator.TranslateToService(domainTeam);
        }

        public Team AddTeam(Team team)
        {
            var domainTeam = _translator.TranslateToDomainNew(team);
            _teamRepository.Save(domainTeam);
            return _translator.TranslateToService(domainTeam);
        }

        public IEnumerable<Tournament> GetAllTournaments()
        {
            var domainTournaments = _tournamentRepository.GetAllTournaments();
            return domainTournaments.Select(_translator.TranslateToService).ToList();
        }

        public Tournament GetTournament(Guid id)
        {
            var domainTournament = _tournamentRepository.GetTournament(id);
            return _translator.TranslateToService(domainTournament);
        }

        public void UploadFile(string file)
        {
            _fileRepository.Upload(file);
        }

        public Tournament AddTournament(Tournament tournament)
        {
            var domainTournament = _translator.TranslateToDomain(tournament);

            _tournamentRepository.Save(domainTournament);
            return _translator.TranslateToService(domainTournament);
        }

        public void SetFixtureResult(Guid tournamentId, Guid fixtureId, IEnumerable<MatchResult> results)
        {
            var tournament = _tournamentRepository.GetTournament(tournamentId);

            tournament.SetFixtureResult(fixtureId, results.Select(item => _translator.TranslateToDomain(item)).ToList());

            _tournamentRepository.Save(tournament);
        }

        public IEnumerable<User> GetAllUsers()
        {
            var domainUsers = _userRepository.GetAllUsers();
            return domainUsers.Select(_translator.TranslateToService).ToList();
        }
    }
}

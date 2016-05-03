using System;
using System.Collections.Generic;
using System.Linq;
using Tsc.Application.ServiceModel;
using Tsc.Domain.ExternalServices;
using Unity;


namespace Tsc.Application
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class TscApplication : ITscApplication
    {
        private ITeamRepository _teamRepository;
        private ITranslator _translator;
        private ITournamentRepository _tournamentRepository;

        public TscApplication()
        {
            var container = GetDefaultContainer();

            _teamRepository = container.Resolve<ITeamRepository>();
            _tournamentRepository = container.Resolve<ITournamentRepository>();
            _translator = container.Resolve<ITranslator>();
        }

        public TscApplication(ITeamRepository teamRepository, ITournamentRepository tournamentRepository, ITranslator translator)
        {
            _teamRepository = teamRepository;
            _tournamentRepository = tournamentRepository;
            _translator = translator;
        }

        private static IUnityContainer GetDefaultContainer()
        {
            var unityContainer = new UnityContainer();
            var dependencyConfigurator = new DependencyConfigurator();
            dependencyConfigurator.Configure(unityContainer);
            return unityContainer;
        }

        public void AddTeam(Team team)
        {
            var domainTeam = _translator.TranslateToDomainNew(team);

            _teamRepository.Save(domainTeam);
        }

        public void AddTournament(Tournament tournament)
        {
            var domainTournament = _translator.TranslateToDomain(tournament);

            _tournamentRepository.Save(domainTournament);
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

        public IEnumerable<Team> GetAllTeams()
        {
            var domainTeams = _teamRepository.GetAllTeams();
            return domainTeams.Select(_translator.TranslateToService).ToList();
        }
    }
}

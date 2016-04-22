using System;
using System.Collections.Generic;
using Tsc.Domain;
using Tsc.Domain.ExternalServices;
using Unity;


namespace Tsc.Application
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class TscApplication
    {
        private ITeamRepository _teamRepository;
        private ITournamentRepository _tournamentRepository;

        public TscApplication()
        {
            var container = GetDefaultContainer();

            _teamRepository = container.Resolve<ITeamRepository>();
            _tournamentRepository = container.Resolve<ITournamentRepository>();
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _teamRepository.GetAllTeams();
        }

        public TscApplication(ITeamRepository teamRepository, ITournamentRepository tournamentRepository)
        {
            _teamRepository = teamRepository;
            _tournamentRepository = tournamentRepository;
        }

        private static IUnityContainer GetDefaultContainer()
        {
            var unityContainer = new UnityContainer();
            var dependencyConfigurator = new DependencyConfigurator();
            dependencyConfigurator.Configure(unityContainer);
            return unityContainer;
        }
    }
}

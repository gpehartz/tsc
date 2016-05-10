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
        private ITranslator _translator;
        private IUserRepository _userRepository;
        private ITeamRepository _teamRepository;
        private ITournamentRepository _tournamentRepository;
        private IFileRepository _fileRepository;

        public TscApplication()
        {
            var container = GetDefaultContainer();

            _userRepository = container.Resolve<IUserRepository>();
            _teamRepository = container.Resolve<ITeamRepository>();
            _tournamentRepository = container.Resolve<ITournamentRepository>();
            _fileRepository = container.Resolve<IFileRepository>();
            _translator = container.Resolve<ITranslator>();
        }

        public TscApplication(IUserRepository userRepository, ITeamRepository teamRepository, ITournamentRepository tournamentRepository, ITranslator translator)
        {
            _userRepository = userRepository;
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
            var domainTournament = _translator.TranslateToDomainNew(tournament);

            _tournamentRepository.Save(domainTournament);
        }

        public void UploadFile(string file)
        {
            _fileRepository.Upload(file);
        }

        public IEnumerable<Tournament> GetAllTournaments()
        {
            var domainTournaments = _tournamentRepository.GetAllTournaments();
            return domainTournaments.Select(_translator.TranslateToService).ToList();
        }

        public IEnumerable<Team> GetAllTeams()
        {
            var domainTeams = _teamRepository.GetAllTeams();
            return domainTeams.Select(_translator.TranslateToService).ToList();
        }

        public IEnumerable<User> GetAllUsers()
        {
            var domainUsers = _userRepository.GetAllUsers();
            return domainUsers.Select(_translator.TranslateToService).ToList();
        }
    }
}

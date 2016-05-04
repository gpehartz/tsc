using Tsc.Application.Services;
using Tsc.Domain.ExternalServices;
using Unity;

namespace Tsc.Application
{
    public class DependencyConfigurator
    {
        internal void Configure(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IUserRepository, UserRepository>();
            unityContainer.RegisterType<ITeamRepository, TeamRepository>();
            unityContainer.RegisterType<ITournamentRepository, TournamentRepository>();
            unityContainer.RegisterType<ITranslator, Translator>();
        }
    }
}

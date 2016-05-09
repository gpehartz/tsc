using Tsc.Application.Services;
using Tsc.DataAccess;
using Tsc.Domain.ExternalServices;
using Unity;

namespace Tsc.Application
{
    public class DependencyConfigurator
    {
        internal void Configure(IUnityContainer unityContainer)
        {
            //in-memory repositories
            //unityContainer.RegisterType<ITeamRepository, TeamRepository>();
            //unityContainer.RegisterType<ITournamentRepository, TournamentRepository>();

            //persistent repositories
            unityContainer.RegisterType<ITscDataAccess, MongoRestTscDataAccess>(new InjectionConstructor(@"http://localhost:3000/"));
            unityContainer.RegisterType<ITeamRepository, PersistentTeamRepository>();
            unityContainer.RegisterType<ITournamentRepository, PersistentTournamentRepository>();


            unityContainer.RegisterType<ITranslator, Translator>();
        }
    }
}

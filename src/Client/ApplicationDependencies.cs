using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tsc.Application;
using Tsc.DataAccess;
using Tsc.Domain.ExternalServices;
using Tsc.Application.Services;

namespace Client
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection serviceCollection, bool usePersistenRepos = true)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException("serviceCollection");

            if (usePersistenRepos)
            {
                //persistent repositories
                serviceCollection.TryAdd(ServiceDescriptor.Singleton<ITscDataAccess, MongoRestTscDataAccess>());
                serviceCollection.TryAdd(ServiceDescriptor.Transient<ITeamRepository, PersistentTeamRepository>());
                serviceCollection.TryAdd(ServiceDescriptor.Transient<ITournamentRepository, PersistentTournamentRepository>());
            }
            else
            {
                //in-memory repositories
                serviceCollection.TryAdd(ServiceDescriptor.Transient<ITeamRepository, TeamRepository>());
                serviceCollection.TryAdd(ServiceDescriptor.Transient<ITournamentRepository, TournamentRepository>());
                serviceCollection.TryAdd(ServiceDescriptor.Transient<IUserRepository, UserRepository>());
            }

            serviceCollection.TryAdd(ServiceDescriptor.Transient<ITranslator, Translator>());
            serviceCollection.TryAdd(ServiceDescriptor.Transient<ITscApplication, TscApplication>());

            return serviceCollection;
        }
    }
}

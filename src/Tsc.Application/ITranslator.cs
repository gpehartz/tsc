namespace Tsc.Application
{
    public interface ITranslator
    {
        Domain.User TranslateToDomain(ServiceModel.User user);

        Domain.Team TranslateToDomain(ServiceModel.Team team);

        Domain.Team TranslateToDomainNew(ServiceModel.Team team);

        Domain.Tournament TranslateToDomain(ServiceModel.Tournament tournament);
        
        ServiceModel.User TranslateToService(Domain.User user);

        ServiceModel.Tournament TranslateToService(Domain.Tournament tournament);

        ServiceModel.Team TranslateToService(Domain.Team team);

        ServiceModel.Tournament TranslateToService(Domain.Tournament tournament);

        Domain.MatchResult TranslateToDomain(ServiceModel.MatchResult matchResult);
    }
}
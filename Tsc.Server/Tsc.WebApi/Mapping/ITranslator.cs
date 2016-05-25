using Tsc.WebApi.ServiceModel;

namespace Tsc.WebApi.Mapping
{
    public interface ITranslator
    {
        Domain.Player TranslateToDomain(Player player);

        Domain.Team TranslateToDomain(Team team);

        Domain.Team TranslateToDomainNew(Team team);

        Domain.Tournament TranslateToDomain(Tournament tournament);

        Player TranslateToService(Domain.Player player);
        
        Team TranslateToService(Domain.Team team);

        Tournament TranslateToService(Domain.Tournament tournament);

        Domain.MatchResult TranslateToDomain(MatchResult matchResult);
    }
}
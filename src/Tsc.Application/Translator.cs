using System.Linq;

namespace Tsc.Application
{
    public class Translator : ITranslator
    {
        public Domain.Tournament TranslateToDomain(ServiceModel.Tournament tournament)
        {
            var domainTournament = new Domain.Tournament(tournament.Id, tournament.Name, tournament.CreationDate);
            domainTournament.AddParticipants(tournament.Participants.Select(TranslateToDomain));

            return domainTournament;
        }

        public Domain.Tournament TranslateToDomainNew(ServiceModel.Tournament tournament)
        {
            var domainTournament = new Domain.Tournament(tournament.Name);
            domainTournament.AddParticipants(tournament.Participants.Select(TranslateToDomain));

            return domainTournament;
        }

        public ServiceModel.Tournament TranslateToService(Domain.Tournament tournament)
        {
            return new ServiceModel.Tournament
            {
                Id = tournament.Id,
                Name = tournament.Name,
                Participants = tournament.Participants.Select(TranslateToService).ToList(),
                CreationDate = tournament.CreationDate
            };
        }

        public Domain.Team TranslateToDomain(ServiceModel.Team team)
        {
            return new Domain.Team(team.Id, team.Name);
        }

        public ServiceModel.Team TranslateToService(Domain.Team team)
        {
            return new ServiceModel.Team
            {
                Id = team.Id,
                Name = team.Name
            };
        }
    }
}

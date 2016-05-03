using System.Linq;

namespace Tsc.Application
{
    public class Translator : ITranslator
    {
        public Domain.Tournament TranslateToDomain(ServiceModel.Tournament tournament)
        {
            var domainTournament = new Domain.Tournament(tournament.Name, tournament.Participants.Select(TranslateToDomain));
            return domainTournament;
        }

        public ServiceModel.Tournament TranslateToService(Domain.Tournament tournament)
        {
            return new ServiceModel.Tournament
            {
                Id = tournament.Id,
                Name = tournament.Name,
                Participants = tournament.Participants.Select(TranslateToService).ToList(),
                CreationDate = tournament.CreationDate,
                Rounds = tournament.Rounds.Select(TranslateToService).ToList(),
                Table = tournament.Table.Select(TranslateToService).ToList()
            };
        }

        public ServiceModel.Round TranslateToService(Domain.Round round)
        {
            return new ServiceModel.Round
            {
                Number = round.Number,
                Fixtures = round.Fixtures.Select(TranslateToService).ToList()
            };
        }

        public ServiceModel.FixtureItem TranslateToService(Domain.FixtureItem fixtureItem)
        {
            return new ServiceModel.FixtureItem
            {
                Id = fixtureItem.Id,
                HomeTeam = fixtureItem.HomeTeam.Name,
                AwayTeam = fixtureItem.AwayTeam.Name
            };
        }

        public ServiceModel.TournamentResultItem TranslateToService(Domain.TournamentResultItem resultItem)
        {
            return new ServiceModel.TournamentResultItem
            {
                Position = resultItem.Position,
                TeamName = resultItem.TeamName,
                Points = resultItem.Points,
                Played = resultItem.Played,
                Won = resultItem.Won,
                Drawn = resultItem.Drawn,
                Lost = resultItem.Lost,
                GoalsFor = resultItem.GoalsFor,
                GoalsAgainst = resultItem.GoalsAgainst,
                GoalDifference = resultItem.GoalDifference
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

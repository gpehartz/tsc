using System.Linq;

namespace Tsc.Application
{
    public class Translator : ITranslator
    {
        public Domain.User TranslateToDomain(ServiceModel.User user)
        {
            return new Domain.User(user.Id, user.UserName, user.CreationDate);
        }

        public Domain.Team TranslateToDomain(ServiceModel.Team team)
        {
            return new Domain.Team(team.Id, team.Name, team.CreationDate);
        }

        public Domain.Team TranslateToDomainNew(ServiceModel.Team team)
        {
            return new Domain.Team(team.Name, team.Users.Select(TranslateToDomain), team.LogoUrl);
        }

        public Domain.Tournament TranslateToDomain(ServiceModel.Tournament tournament)
        {
            var domainTournament = new Domain.Tournament(tournament.Name, tournament.Participants.Select(TranslateToDomain), tournament.LogoUrl);
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
                Table = tournament.Table.Select(TranslateToService).ToList(),
                LogoUrl = tournament.LogoUrl
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

        public static ServiceModel.Fixture TranslateToService(Domain.Fixture fixture)
        {
            return new ServiceModel.Fixture
            {
                Id = fixture.Id,
                HomeTeam = fixture.HomeTeam.Name,
                AwayTeam = fixture.AwayTeam.Name,
                HasResult = fixture.HasResult,
                Results = fixture.Results.Select(TranslateToService).ToList()
            };
        }

        public static ServiceModel.MatchResult TranslateToService(Domain.MatchResult matchResult)
        {
            return new ServiceModel.MatchResult
                   {
                       HomeGoals = matchResult.HomeGoals,
                       AwayGoals = matchResult.AwayGoals
                   };
        }

        public static ServiceModel.TournamentResultItem TranslateToService(Domain.TournamentResultItem resultItem)
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

        public ServiceModel.Team TranslateToService(Domain.Team team)
        {
            return new ServiceModel.Team
            {
                Id = team.Id,
                Name = team.Name,
                Users =  team.Users.Select(TranslateToService).ToList(),
                CreationDate = team.CreationDate,
                LogoUrl = team.LogoUrl
            };
        }

        public ServiceModel.User TranslateToService(Domain.User user)
        {
            return new ServiceModel.User
            {
                Id = user.Id,
                UserName = user.UserName,
                CreationDate = user.CreationDate
            };
        }

        public Domain.MatchResult TranslateToDomain(ServiceModel.MatchResult matchResult)
        {
            return new Domain.MatchResult
                   {
                       HomeGoals = matchResult.HomeGoals,
                       AwayGoals = matchResult.AwayGoals
                   };
        }
    }
}

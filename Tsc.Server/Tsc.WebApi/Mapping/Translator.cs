using System.Linq;
using Tsc.WebApi.ServiceModel;

namespace Tsc.WebApi.Mapping
{
    public class Translator : ITranslator
    {
        public Domain.Player TranslateToDomain(Player player)
        {
            return new Domain.Player(player.Id, player.UserName, player.CreationDate);
        }

        public Domain.Team TranslateToDomain(Team team)
        {
            return new Domain.Team(team.Id, team.Name, team.CreationDate);
        }

        public Domain.Team TranslateToDomainNew(Team team)
        {
            var players = team.Players.Select(TranslateToDomain).ToList();
            return new Domain.Team(team.Name, players, team.LogoUrl);
        }

        public Domain.Tournament TranslateToDomain(Tournament tournament)
        {
            var participants = tournament.Participants.Select(TranslateToDomain).ToList();
            return new Domain.Tournament(tournament.Name, participants, tournament.LogoUrl);
        }

        public Tournament TranslateToService(Domain.Tournament tournament)
        {
            return
                new Tournament
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

        public Round TranslateToService(Domain.Round round)
        {
            return
                new Round
                {
                    Number = round.Number,
                    Fixtures = round.Fixtures.Select(TranslateToService).ToList()
                };
        }

        public static Fixture TranslateToService(Domain.Fixture fixture)
        {
            return
                new Fixture
                {
                    Id = fixture.Id,
                    HomeTeam = fixture.HomeTeam.Name,
                    AwayTeam = fixture.AwayTeam.Name,
                    HasResult = fixture.HasResult,
                    Results = fixture.Results.Select(TranslateToService).ToList()
                };
        }

        public static MatchResult TranslateToService(Domain.MatchResult matchResult)
        {
            return
                new MatchResult
                {
                    HomeGoals = matchResult.HomeGoals,
                    AwayGoals = matchResult.AwayGoals
                };
        }

        public static TournamentResultItem TranslateToService(Domain.TournamentResultItem resultItem)
        {
            return
                new TournamentResultItem
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

        public Team TranslateToService(Domain.Team team)
        {
            return
                new Team
                {
                    Id = team.Id,
                    Name = team.Name,
                    Players = team.Players.Select(TranslateToService).ToList(),
                    CreationDate = team.CreationDate,
                    LogoUrl = team.LogoUrl
                };
        }

        public Player TranslateToService(Domain.Player player)
        {
            return
                new Player
                {
                    Id = player.Id,
                    UserName = player.PlayerName,
                    CreationDate = player.CreationDate
                };
        }

        public Domain.MatchResult TranslateToDomain(MatchResult matchResult)
        {
            return
                new Domain.MatchResult
                {
                    HomeGoals = matchResult.HomeGoals,
                    AwayGoals = matchResult.AwayGoals
                };
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Tsc.Domain.InternalServices
{
    public class ResultTableEnumeratorService : IResultTableEnumeratorService
    {
        public IEnumerable<TournamentResultItem> Create(IEnumerable<Round> rounds, IEnumerable<Team> participants)
        {
            var result = new List<TournamentResultItem>();

            var fixtures = rounds.SelectMany(item => item.Fixtures).ToList();

            if (!fixtures.Any(item => item.HasResult))
            {
                result = CreateEmptyResultTable(participants).ToList();
                return result;
            }

            foreach (var fixture in fixtures)
            {
                var homeTeamResultRow = GetOrCreateTeamResultRow(result, fixture.HomeTeam);
                var awayTeamResultRow = GetOrCreateTeamResultRow(result, fixture.AwayTeam);

                if (fixture.Results.Any())
                {
                    homeTeamResultRow.Played++;
                    awayTeamResultRow.Played++;
                }

                foreach (var matchResult in fixture.Results)
                {
                    homeTeamResultRow.GoalsFor += matchResult.HomeGoals;
                    homeTeamResultRow.GoalsAgainst += matchResult.AwayGoals;
                    awayTeamResultRow.GoalsFor += matchResult.AwayGoals;
                    awayTeamResultRow.GoalsAgainst += matchResult.HomeGoals;

                    if (matchResult.HomeGoals > matchResult.AwayGoals)
                    {
                        homeTeamResultRow.Points += 2;
                        homeTeamResultRow.Won++;
                        awayTeamResultRow.Lost++;
                    }
                    else if (matchResult.HomeGoals == matchResult.AwayGoals)
                    {
                        homeTeamResultRow.Points++;
                        homeTeamResultRow.Drawn++;
                        awayTeamResultRow.Points++;
                        awayTeamResultRow.Drawn++;
                    }
                    else
                    {
                        homeTeamResultRow.Lost++;
                        awayTeamResultRow.Points += 2;
                        awayTeamResultRow.Won++;
                    }
                }

                AddOrUpdateTeamResultRow(result, homeTeamResultRow);
                AddOrUpdateTeamResultRow(result, awayTeamResultRow);
            }

            result =
                result.OrderByDescending(item => item.Points)
                      .ThenByDescending(item => item.Won)
                      .ThenByDescending(item => item.Drawn)
                      .ThenByDescending(item => item.GoalsFor)
                      .ThenByDescending(item => item.Played)
                      .ToList();

            var index = 1;
            foreach (var tournamentResultItem in result)
            {
                tournamentResultItem.Position = index;
                index++;
            }

            return result;
        }

        private static void AddOrUpdateTeamResultRow(ICollection<TournamentResultItem> result, TournamentResultItem resultItem)
        {
            var existingResultItem = result.FirstOrDefault(item => item.Team.Id == resultItem.Team.Id);

            if (existingResultItem != null)
            {
                result.Remove(existingResultItem);
            }

            result.Add(resultItem);
        }

        private static TournamentResultItem GetOrCreateTeamResultRow(IEnumerable<TournamentResultItem> tournamentResults, Team team)
        {
            var result = tournamentResults.FirstOrDefault(item => item.Team.Id == team.Id);

            if (result == null)
            {
                return new TournamentResultItem
                       {
                           Team = team
                       };
            }
            return result;
        }

        private static IEnumerable<TournamentResultItem> CreateEmptyResultTable(IEnumerable<Team> participants)
        {
            var result = new List<TournamentResultItem>();

            var index = 1;
            foreach (var participant in participants.OrderBy(item => item.Name))
            {
                result.Add(new TournamentResultItem
                {
                    Position = index,
                    Team = participant
                });

                index++;
            }

            return result;
        }
    }
}

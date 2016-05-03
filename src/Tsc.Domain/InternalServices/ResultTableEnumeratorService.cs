using System.Collections.Generic;
using System.Linq;

namespace Tsc.Domain.InternalServices
{
    public class ResultTableEnumeratorService : IResultTableEnumeratorService
    {
        public IEnumerable<TournamentResultItem> Create(IEnumerable<FixtureResult> fixtureResults, IEnumerable<Team> participants)
        {
            var result = new List<TournamentResultItem>();

            if (fixtureResults == null || !fixtureResults.Any())
            {
                result = CreateEmptyResultTable(participants).ToList();
            }

            return result;
        }

        private IEnumerable<TournamentResultItem> CreateEmptyResultTable(IEnumerable<Team> participants)
        {
            var result = new List<TournamentResultItem>();

            var index = 1;
            foreach (var participant in participants.OrderBy(item => item.Name))
            {
                result.Add(new TournamentResultItem
                {
                    Position = index,
                    TeamName = participant.Name
                });

                index++;
            }

            return result;
        }
    }
}

using System.Collections.Generic;

namespace Tsc.Domain.InternalServices
{
    public interface IResultTableEnumeratorService
    {
        IEnumerable<TournamentResultItem> Create(IEnumerable<FixtureResult> fixtureResults, IEnumerable<Team> participants);
    }
}

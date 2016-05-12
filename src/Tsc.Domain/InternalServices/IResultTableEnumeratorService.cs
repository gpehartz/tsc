using System.Collections.Generic;

namespace Tsc.Domain.InternalServices
{
    public interface IResultTableEnumeratorService
    {
        IEnumerable<TournamentResultItem> Create(IEnumerable<Round> rounds, IEnumerable<Team> participants);
    }
}

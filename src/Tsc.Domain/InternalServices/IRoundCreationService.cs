using System.Collections.Generic;

namespace Tsc.Domain.InternalServices
{
    public interface IRoundCreationService
    {
        IEnumerable<Round> CreateRounds(IEnumerable<Team> participantTeams);
    }
}

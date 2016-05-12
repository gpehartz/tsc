using System.Collections.Generic;

namespace Tsc.Domain.InternalServices
{
    public interface IParticipantEnlisterService
    {
        IEnumerable<Team> GetParticipantsForFixtureCreation(IEnumerable<Team> selectedTeams);
    }
}

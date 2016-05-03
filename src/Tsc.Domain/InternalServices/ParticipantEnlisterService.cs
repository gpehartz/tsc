using System.Collections.Generic;
using System.Linq;

namespace Tsc.Domain.InternalServices
{
    public class ParticipantEnlisterService : IParticipantEnlisterService
    {
        public IEnumerable<Team> GetParticipantsForFixtureCreation(IEnumerable<Team> selectedTeams)
        {
            var result = new List<Team>();

            result = selectedTeams.OrderBy(item => item.Name).ToList();

            if (IsOdd(result.Count))
            {
                result.Add(Team.DummyTeam);
            }

            return result;
        }

        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }
    }
}

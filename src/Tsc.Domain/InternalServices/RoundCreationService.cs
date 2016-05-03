using System.Collections.Generic;
using System.Linq;

namespace Tsc.Domain.InternalServices
{
    public class RoundCreationService : IRoundCreationService
    {
        public IEnumerable<Round> CreateRounds(IEnumerable<Team> participantTeams)
        {
            var rounds = CreateRoundsFrom(participantTeams.ToList());
            return rounds;
        }

        private static IEnumerable<Round> CreateRoundsFrom(IReadOnlyList<Team> participants)
        {
            var result = new List<Round>();
            if (!participants.Any()) return result;

            //első forduló
            var round = new Round(1);
            for (var i = 0; i < participants.Count / 2; i++)
            {
                round.AddFixture(new FixtureItem(participants[i], participants[(participants.Count - 1) - i]));
            }
            result.Add(round);

            //további fordulók
            var numberOfRounds = participants.Count - 1;
            var numberofMatchesInOneRound = participants.Count/2;

            var listToShift = participants.Skip(1).ToList();

            for (var i = 1; i < numberOfRounds; i++)
            {
                round = new Round(i + 1);

                var shiftedList = ShiftListItems(listToShift, i);

                round.AddFixture(new FixtureItem(participants[0], shiftedList[(shiftedList.Count) - 1]));

                for (var j = 1; j < numberofMatchesInOneRound; j++)
                {
                    round.AddFixture(new FixtureItem(shiftedList[j-1], shiftedList[(shiftedList.Count) - j - 1]));
                }

                result.Add(round);
            }

            return result;
        }

        private static List<Team> ShiftListItems(IReadOnlyList<Team> list, int offset)
        {
            var resultArray = new Team[list.Count];

            for (var i = 0; i < list.Count; i++)
            {
                var targetPosition = (i + offset) % list.Count;
                resultArray[targetPosition] = list[i];
            }

            return resultArray.ToList();
        }
    }
}

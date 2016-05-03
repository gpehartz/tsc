using System.Collections.Generic;

namespace Tsc.Domain
{
    public class Round
    {
        private readonly List<FixtureItem> _fixtures;

        public int Number { get; private set; }

        public IEnumerable<FixtureItem> Fixtures
        {
            get { return _fixtures.AsReadOnly(); }
        }

        public Round(int roundNumber)
        {
            Number = roundNumber;
            _fixtures = new List<FixtureItem>();
        }

        public void AddFixtures(IEnumerable<FixtureItem> fixturesForRound)
        {
            _fixtures.AddRange(fixturesForRound);
        }

        public void AddFixture(FixtureItem item)
        {
            _fixtures.Add(item);
        }
    }
}

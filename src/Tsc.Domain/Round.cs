using System.Collections.Generic;

namespace Tsc.Domain
{
    public class Round
    {
        private List<FixtureItem> _fixtures;

        public int Number { get; private set; }

        public IEnumerable<FixtureItem> Fixtures
        {
            get { return _fixtures.AsReadOnly(); }
            private set { _fixtures = new List<FixtureItem>(value); }
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

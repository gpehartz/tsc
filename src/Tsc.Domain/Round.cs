using System.Collections.Generic;

namespace Tsc.Domain
{
    public class Round
    {
        private List<Fixture> _fixtures;

        public int Number { get; private set; }

        public IEnumerable<Fixture> Fixtures
        {
            get { return _fixtures.AsReadOnly(); }
            private set { _fixtures = new List<Fixture>(value); }
        }

        public Round(int roundNumber)
        {
            Number = roundNumber;

            _fixtures = new List<Fixture>();
        }

        public void AddFixture(Fixture item)
        {
            _fixtures.Add(item);
        }
    }
}

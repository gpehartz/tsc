using System.Collections.Generic;

namespace Tsc.Domain
{
    public class Round
    {
        private readonly List<Fixture> _fixtures;

        public int Number { get; private set; }

        public IEnumerable<Fixture> Fixtures
        {
            get { return _fixtures.AsReadOnly(); }
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

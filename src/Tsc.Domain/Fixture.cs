using System;
using System.Collections.Generic;
using System.Linq;

namespace Tsc.Domain
{
    public class Fixture
    {
        private readonly List<MatchResult> _results; 

        public Guid Id { get; private set; }

        public Team HomeTeam { get; private set; }

        public Team AwayTeam { get; private set; }

        public bool HasResult
        {
            get { return Results.Any(); }
        }

        public IEnumerable<MatchResult> Results
        {
            get { return _results.AsReadOnly(); }
        }

        public Fixture(Team homeTeam, Team awayTeam)
        {
            Id = Guid.NewGuid();

            HomeTeam = homeTeam;
            AwayTeam = awayTeam;

            _results = new List<MatchResult>();
        }

        public void AddResults(IEnumerable<MatchResult> results)
        {
            _results.AddRange(results);
        }
    }
}

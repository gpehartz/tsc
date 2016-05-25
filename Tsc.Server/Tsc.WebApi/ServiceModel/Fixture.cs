using System;
using System.Collections.Generic;

namespace Tsc.WebApi.ServiceModel
{
    public class Fixture
    {
        public Guid Id { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public bool HasResult { get; set; }

        public List<MatchResult> Results { get; set; }
    }
}

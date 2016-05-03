using System;
using System.Collections.Generic;

namespace Tsc.Domain
{
    public class FixtureResult
    {
        public Guid Id { get; set; }

        public FixtureItem Item { get; set; }

        public List<MatchResult> Results { get; set; }
    }
}

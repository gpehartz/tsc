using System.Collections.Generic;

namespace Tsc.Application.ServiceModel
{
    public class Round
    {
        public int Number { get; set; } 

        public List<FixtureItem> Fixtures { get; set; }
    }
}

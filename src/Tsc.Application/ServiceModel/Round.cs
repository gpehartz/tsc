using System.Collections.Generic;

namespace Tsc.Application.ServiceModel
{
    public class Round
    {
        public int Number { get; set; } 

        public List<Fixture> Fixtures { get; set; }
    }
}

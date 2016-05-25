using System.Collections.Generic;

namespace Tsc.WebApi.ServiceModel
{
    public class Round
    {
        public int Number { get; set; } 

        public List<Fixture> Fixtures { get; set; }
    }
}

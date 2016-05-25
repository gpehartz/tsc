using System;
using System.Collections.Generic;

namespace Tsc.WebApi.ServiceModel
{
    public class Team
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public IEnumerable<Player> Players { get; set; }

        public string LogoUrl { get; set; }
    }
}

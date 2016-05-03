using System;
using System.Collections.Generic;

namespace Tsc.Application.ServiceModel
{
    public class Team
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}

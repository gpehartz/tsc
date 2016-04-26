using System;

namespace Tsc.Domain
{
    public class Team
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public Team(Guid id, string name)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Name = name;
        }
    }
}

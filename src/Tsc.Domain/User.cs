using System;

namespace Tsc.Domain
{
    public class User
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public User(Guid id, string name)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Name = name;
        }
    }
}

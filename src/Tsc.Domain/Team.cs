using System;
using System.Collections.Generic;

namespace Tsc.Domain
{
    public class Team
    {
        private List<User> _users;

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public DateTime CreationDate { get; private set; }

        public IEnumerable<User> Users
        {
            get
            {
                return _users.AsReadOnly();
            }
        }

        public void AddUser(IEnumerable<User> users)
        {
            _users.AddRange(users);
        }

        /// <summary>
        /// For existing
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="creationDate"></param>
        public Team(Guid id, string name, DateTime creationDate)
            :this()
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;
        }

        /// <summary>
        /// For new
        /// </summary>
        /// <param name="name"></param>
        public Team(string name)
            :this()
        {
            Name = name;

            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        private Team()
        {
            _users = new List<User>();
        }

        public static Team DummyTeam = new Team(Guid.NewGuid(), "N/A");
    }
}

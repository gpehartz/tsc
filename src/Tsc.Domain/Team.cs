using System;
using System.Collections.Generic;

namespace Tsc.Domain
{
    public class Team : IIdentifiable
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
            private set
            {
                _users = new List<User>(value);
            }
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
        /// <param name="users"></param>
        public Team(string name, IEnumerable<User> users)
            :this()
        {
            Name = name;

            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;

            _users.AddRange(users);
        }

        private Team()
        {
            _users = new List<User>();
        }

        public static readonly Team DummyTeam = new Team("N/A", new List<User>());
    }
}

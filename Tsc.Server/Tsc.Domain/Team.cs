using System;
using System.Collections.Generic;

namespace Tsc.Domain
{
    public class Team : IIdentifiable
    {
        private List<Player> _players;

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public DateTime CreationDate { get; private set; }

        public string LogoUrl { get; private set; }

        public IEnumerable<Player> Players
        {
            get
            {
                return _players.AsReadOnly();
            }
            private set
            {
                _players = new List<Player>(value);
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
        /// <param name="players"></param>
        /// <param name="logoUrl"></param>
        public Team(string name, IEnumerable<Player> players, string logoUrl)
            :this()
        {
            Name = name;

            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
            LogoUrl = logoUrl;

            _players.AddRange(players);
        }

        private Team()
        {
            _players = new List<Player>();
        }

        public static readonly Team DummyTeam = new Team("N/A", new List<Player>(), string.Empty);
    }
}

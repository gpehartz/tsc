using System;
using System.Collections.Generic;

namespace Tsc.Domain
{
    public class Tournament
    {
        private List<Team> _participants;

        //For existing
        public Tournament(Guid id, string name, DateTime creationDate)
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;

            _participants = new List<Team>();
        }

        //For new
        public Tournament(string name)
        {
            Name = name;

            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;

            _participants = new List<Team>();
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public DateTime CreationDate { get; private set; }

        public IEnumerable<Team> Participants
        {
            get
            {
                return _participants.AsReadOnly();
            }
        }

        public void AddParticipants(IEnumerable<Team> participants)
        {
            _participants.AddRange(participants);
        }
    }
}
        
using System;

namespace Tsc.Domain
{
    public class Player
    {
        public Guid Id { get; private set; }

        public string PlayerName { get; private set; }
        
        public DateTime CreationDate { get; private set; }
        
        /// <summary>
        /// For existing
        /// </summary>
        /// <param name="id"></param>
        /// <param name="playerName"></param>
        /// <param name="creationDate"></param>
        public Player(Guid id, string playerName, DateTime creationDate)
        {
            Id = id;
            PlayerName = playerName;
            CreationDate = creationDate;
        }

        /// <summary>
        /// For new
        /// </summary>
        /// <param name="playerName"></param>
        public Player(string playerName)
            : this(Guid.NewGuid(), playerName, DateTime.Now)
        {
        }
    }
}

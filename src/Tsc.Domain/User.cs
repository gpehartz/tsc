using System;

namespace Tsc.Domain
{
    public class User
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }
        
        public DateTime CreationDate { get; private set; }
        
        /// <summary>
        /// For existing
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="creationDate"></param>
        public User(Guid id, string name, DateTime creationDate)
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;
        }

        /// <summary>
        /// For new
        /// </summary>
        /// <param name="name"></param>
        public User(string name)
        {
            Name = name;

            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
    }
}

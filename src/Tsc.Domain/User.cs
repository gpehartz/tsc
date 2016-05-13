using System;

namespace Tsc.Domain
{
    public class User
    {
        public Guid Id { get; private set; }

        public string UserName { get; private set; }
        
        public DateTime CreationDate { get; private set; }
        
        /// <summary>
        /// For existing
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="creationDate"></param>
        public User(Guid id, string userName, DateTime creationDate)
        {
            Id = id;
            UserName = userName;
            CreationDate = creationDate;
        }

        /// <summary>
        /// For new
        /// </summary>
        /// <param name="userName"></param>
        public User(string userName)
        {
            UserName = userName;

            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
    }
}

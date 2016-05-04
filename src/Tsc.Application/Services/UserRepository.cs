using System.Collections.Generic;
using Tsc.Domain;
using Tsc.Domain.ExternalServices;

namespace Tsc.Application.Services
{
    public class UserRepository : IUserRepository
    {
        private static List<User> _users = new List<User>
            {
                new User("King_Geedorah87"),
                new User("MrLions"),
                new User("wayne_kenoff"),
                new User("IMadeYouReadThis"),
                new User("theSodommizer"),
                new User("Thrubeingcool13"),
                new User("lol_haha_dead"),
                new User("Omni-Slash")
            };
        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public void Save(User user)
        {
            _users.Add(user);
        }
    }
}

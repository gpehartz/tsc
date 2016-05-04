using System.Collections.Generic;

namespace Tsc.Domain.ExternalServices
{
    public interface IUserRepository
    {
        void Save(User user);
        IEnumerable<User> GetAllUsers();
    }
}

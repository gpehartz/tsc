using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Tsc.Identity;

namespace Tsc.Application.Repositories
{
    public class UserStore<TUser> : UserStore<TUser, Guid> where TUser : IdentityUser<Guid>
    {
    }

    /// <summary>
    /// Class that implements the key ASP.NET Identity user store iterfaces
    /// </summary>
    public class UserStore<TUser, TKey> : 
        IUserStore<TUser>,
        IUserLoginStore<TUser>,
        IQueryableUserStore<TUser>,
        IUserRoleStore<TUser>,
        IUserClaimStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserSecurityStampStore<TUser>,
        IUserEmailStore<TUser>,
        IUserLockoutStore<TUser>,
        IUserPhoneNumberStore<TUser>,
        IUserTwoFactorStore<TUser>, 
        IDisposable
        where TUser : IdentityUser<TKey> 
        where TKey : IEquatable<TKey>
    {
        private readonly InMemoryDatabase _database;

        public IQueryable<TUser> Users
        {
            get { return _database.UsersTable<TUser>().AsQueryable(); }
        }

        /// <summary>
        /// Default constructor that initializes a new InMemoryDatabase instance.
        /// </summary>
        public UserStore()
            :this(new InMemoryDatabase())
        {
        }

        /// <summary>
        /// Constructor that takes a InMemoryDatabase as argument.
        /// </summary>
        /// <param name="database"></param>
        public UserStore(InMemoryDatabase database)
        {
            _database = database;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
        }

        #endregion

        #region IUserStore<TUser> members 

        public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.UserName = userName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            _database.UsersTable<TUser>().Add(user);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            _database.UsersTable<TUser>().Update(user, i => Equals(i.Id, user.Id));
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            _database.UsersTable<TUser>().Remove(user);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Null or empty argument: userId");
            }

            TUser result = _database.UsersTable<TUser>().Find(i => Equals(i.Id, userId));
            return Task.FromResult(result);
        }

        public Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(normalizedUserName))
            {
                throw new ArgumentException("Null or empty argument: normalizedUserName");
            }

            var user = _database.UsersTable<TUser>().FirstOrDefault(i => i.UserName == normalizedUserName);
            return Task.FromResult(user);
        }

        #endregion IUserStore<TUser> members 

        #region IUserRoleStore<TUser> members 

        // generic item of _database.UserRolesTable
        private class UserRoles
        {
            public string Role { get; private set; }
            public HashSet<TUser> Users { get; private set; }

            public UserRoles(string role)
            {
                Role = role;
                Users = new HashSet<TUser>();
            }
        }

        public Task AddToRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            var userRoles = _database.UserRolesTable<UserRoles>().Find(i => i.Role == roleName);
            if (userRoles == null)
            {
                userRoles = new UserRoles(roleName);
                _database.UserRolesTable<UserRoles>().Add(userRoles);
            }

            userRoles.Users.Add(user);
            return Task.FromResult<object>(null);
        }

        public Task RemoveFromRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            var userRoles = _database.UserRolesTable<UserRoles>().Find(i => i.Role == roleName);
            if (userRoles != null)
            {
                userRoles.Users.Remove(user);
            }

            return Task.FromResult<object>(null);
        }

        public Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var userRoles =
                _database.UserRolesTable<UserRoles>()
                    .Where(i => i.Users.Any(u => Equals(u.Id, user.Id)))
                    .Select(i => i.Role)
                    .ToList();

            return Task.FromResult<IList<string>>(userRoles);
        }

        public Task<bool> IsInRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            var userRoles = _database.UserRolesTable<UserRoles>().Find(i => i.Role == roleName);
            var result = userRoles != null && userRoles.Users.Any(u => Equals(u.Id, user.Id));

            return Task.FromResult(result);
        }

        public Task<IList<TUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            var userRoles = _database.UserRolesTable<UserRoles>().Find(i => i.Role == roleName);
            var result = userRoles != null ? userRoles.Users : new HashSet<TUser>();

            return Task.FromResult<IList<TUser>>(result.ToList());
        }

        #endregion IUserRoleStore<TUser> members 

        #region IUserPasswordStore<TUser> members 

        public Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult<object>(null);
        }

        public Task<string> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(string.IsNullOrEmpty(user.PasswordHash));
        }

        #endregion IUserPasswordStore<TUser> members 

        #region IUserSecurityStampStore<TUser> members 

        public Task SetSecurityStampAsync(TUser user, string stamp, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.SecurityStamp);
        }

        #endregion IUserSecurityStampStore<TUser> members 

        #region IUserEmailStore<TUser> members 

        public Task SetEmailAsync(TUser user, string email, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(normalizedEmail))
            {
                throw new ArgumentException("NormalizedEmail should not be null or whitespace!");
            }

            return Task.FromResult(_database.UsersTable<TUser>().FirstOrDefault(u => u.NormalizedEmail == normalizedEmail));
        }

        public Task<string> GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(TUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        #endregion IUserEmailStore<TUser> members 

        #region Implementation of IUserPhoneNumberStore<TUser>

        public Task SetPhoneNumberAsync(TUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        #endregion Implementation of IUserPhoneNumberStore<TUser>

        #region Implementation of IUserLoginStore<TUser>

        // generic item of _database.UserLoginsTable
        private class UserLoginAssociation
        {
            public TUser User { get; private set; }
            public UserLoginInfo LoginInfo { get; private set; }

            public UserLoginAssociation(TUser user, UserLoginInfo loginInfo)
            {
                User = user;
                LoginInfo = loginInfo;
            }
        }

        public Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            _database.UserLoginsTable<UserLoginAssociation>().Add(new UserLoginAssociation(user, login));
            return Task.FromResult(0);
        }

        public Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            _database.UserLoginsTable<UserLoginAssociation>().RemoveAll(i => i.LoginInfo.LoginProvider == loginProvider && i.LoginInfo.ProviderKey == providerKey);
            return Task.FromResult(0);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var result =
                _database.UserLoginsTable<UserLoginAssociation>()
                    .Where(i => Equals(i.User.Id, user.Id))
                    .Select(i => i.LoginInfo)
                    .ToList();

            return Task.FromResult<IList<UserLoginInfo>>(result);
        }

        public Task<TUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            var item =
                _database.UserLoginsTable<UserLoginAssociation>()
                    .FirstOrDefault(i => i.LoginInfo.LoginProvider == loginProvider && i.LoginInfo.ProviderKey == providerKey);

            return Task.FromResult(item?.User);
        }

        #endregion

        #region Implementation of IUserClaimStore<TUser>

        // generic item of _database.UserClaimsTable
        private class UserClaimsAssociation
        {
            public TUser User { get; private set; }
            public HashSet<Claim> Claims { get; private set; }

            public UserClaimsAssociation(TUser user)
            {
                User = user;
                Claims = new HashSet<Claim>();
            }
        }

        public Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var result = _database.UserClaimsTable<UserClaimsAssociation>()
                .Where(i => Equals(i.User.Id, user.Id))
                .SelectMany(i => i.Claims)
                .ToList();

            return Task.FromResult<IList<Claim>>(result);
        }

        public Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claims == null)
            {
                throw new ArgumentNullException("claims");
            }

            var userClaimsAssociation = _database.UserClaimsTable<UserClaimsAssociation>().FirstOrDefault(i => Equals(i.User.Id, user.Id));
            if (userClaimsAssociation == null)
            {
                userClaimsAssociation = new UserClaimsAssociation(user);
                _database.UserClaimsTable<UserClaimsAssociation>().Add(userClaimsAssociation);
            }

            foreach (var claim in claims)
            {
                userClaimsAssociation.Claims.Add(claim);
            }

            return Task.FromResult(0);
        }

        public Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            if (newClaim == null)
            {
                throw new ArgumentNullException("newClaim");
            }

            var userClaimsAssociation = _database.UserClaimsTable<UserClaimsAssociation>().FirstOrDefault(i => Equals(i.User.Id, user.Id));
            if (userClaimsAssociation == null)
            {
                userClaimsAssociation = new UserClaimsAssociation(user);
                userClaimsAssociation.Claims.Add(claim);
                _database.UserClaimsTable<UserClaimsAssociation>().Add(userClaimsAssociation);
            }
            else
            {
                var claimList = userClaimsAssociation.Claims.ToList();
                var index = claimList.FindIndex(c => c == claim);
                claimList[index] = newClaim;
            }

            return Task.FromResult(0);
        }

        public Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claims == null)
            {
                throw new ArgumentNullException("claims");
            }

            var userClaimsAssociation = _database.UserClaimsTable<UserClaimsAssociation>().FirstOrDefault(i => Equals(i.User.Id, user.Id));
            if (userClaimsAssociation != null)
            {
                userClaimsAssociation.Claims.RemoveWhere(i => claims.Any(c => c == i));
            }

            return Task.FromResult(0);
        }

        public Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            var result = _database.UserClaimsTable<UserClaimsAssociation>()
                .Where(i => i.Claims.Any(c => c == claim))
                .Select(i => i.User)
                .ToList();

            return Task.FromResult<IList<TUser>>(result);
        }

        #endregion

        #region Implementation of IUserLockoutStore<TUser>

        public Task<DateTimeOffset?> GetLockoutEndDateAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.LockoutEnd);
        }

        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.LockoutEnd = lockoutEnd;
            return Task.FromResult(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.AccessFailedCount += user.AccessFailedCount;
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        #endregion

        #region Implementation of IUserTwoFactorStore<TUser>

        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetTwoFactorEnabledAsync(TUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
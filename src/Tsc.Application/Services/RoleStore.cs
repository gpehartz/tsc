using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Tsc.Application.Services
{
    public class RoleStore<TRole> : RoleStore<TRole, Guid> where TRole : IdentityRole<Guid>
    {
    }

    public class RoleStore<TRole, TKey> : IQueryableRoleStore<TRole>, IRoleStore<TRole>, IRoleClaimStore<TRole>, IDisposable where TRole : IdentityRole<TKey> 
        where TKey : IEquatable<TKey>
    {
        private readonly InMemoryDatabase _database;

        /// <summary>
        /// Default constructor that initializes a new InMemoryDatabase instance.
        /// </summary>
        public RoleStore()
            :this(new InMemoryDatabase())
        {
        }

        /// <summary>
        /// Constructor that takes a InMemoryDatabase as argument.
        /// </summary>
        /// <param name="database"></param>
        public RoleStore(InMemoryDatabase database)
        {
            _database = database;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
        }

        #endregion

        #region Implementation of IRoleStore<TRole>

        public Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            _database.RolesTable<TRole>().Add(role);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            _database.RolesTable<TRole>().Update(role, i => Equals(i.Id, role.Id));
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            _database.RolesTable<TRole>().Remove(role);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            role.Name = roleName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            role.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

        public Task<TRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                throw new ArgumentException("Null or empty argument: roleId");
            }

            TRole result = _database.RolesTable<TRole>().Find(i => Equals(i.Id, roleId));
            return Task.FromResult(result);
        }

        public Task<TRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(normalizedRoleName))
            {
                throw new ArgumentException("Null or empty argument: normalizedRoleName");
            }

            var role = _database.RolesTable<TRole>().FirstOrDefault(i => i.NormalizedName == normalizedRoleName);
            return Task.FromResult(role);
        }

        #endregion

        #region Implementation of IQueryableRoleStore<TRole>

        public IQueryable<TRole> Roles { get; }

        #endregion

        #region Implementation of IRoleClaimStore<TRole>

        // generic item of _database.UserClaimsTable
        private class RoleClaimsAssociation
        {
            public TRole Role { get; private set; }
            public HashSet<Claim> Claims { get; private set; }

            public RoleClaimsAssociation(TRole role)
            {
                Role = role;
                Claims = new HashSet<Claim>();
            }
        }

        public Task<IList<Claim>> GetClaimsAsync(TRole role, CancellationToken cancellationToken = new CancellationToken())
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            var result = _database.RoleClaimsTable<RoleClaimsAssociation>()
                .Where(i => Equals(i.Role.Id, role.Id))
                .SelectMany(i => i.Claims)
                .ToList();

            return Task.FromResult<IList<Claim>>(result);
        }

        public Task AddClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            var roleClaimsAssociation = _database.RoleClaimsTable<RoleClaimsAssociation>().FirstOrDefault(i => Equals(i.Role.Id, role.Id));
            if (roleClaimsAssociation == null)
            {
                roleClaimsAssociation = new RoleClaimsAssociation(role);
                _database.UserClaimsTable<RoleClaimsAssociation>().Add(roleClaimsAssociation);
            }

            roleClaimsAssociation.Claims.Add(claim);
            return Task.FromResult(0);
        }

        public Task RemoveClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

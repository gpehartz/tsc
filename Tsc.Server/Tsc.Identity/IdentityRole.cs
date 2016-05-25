using System;

namespace Tsc.Identity
{
    /// <summary>
    /// Represents a role in the identity system
    /// </summary>
    /// <typeparam name="TKey">The type used for the primary key for the role.</typeparam>
    public class IdentityRole<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="T:Microsoft.AspNet.Identity.EntityFramework.IdentityRole`1"/>.
        /// </summary>
        public IdentityRole()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="T:Microsoft.AspNet.Identity.EntityFramework.IdentityRole`1"/>.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        public IdentityRole(string roleName)
          : this()
        {
            Name = roleName;
        }

        /// <summary>
        /// Gets or sets the primary key for this role.
        /// </summary>
        public virtual TKey Id { get; set; }

        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the normalized name for this role.
        /// </summary>
        public virtual string NormalizedName { get; set; }

        /// <summary>
        /// Returns the name of the role.
        /// </summary>
        /// <returns>
        /// The name of the role.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        /* - EF related
        /// <summary>
        /// A random value that should change whenever a role is persisted to the store
        /// </summary>
        public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Navigation property for the users in this role.
        /// </summary>
        public virtual ICollection<IdentityUserRole<TKey>> Users { get; } = (ICollection<IdentityUserRole<TKey>>)new List<IdentityUserRole<TKey>>();

        /// <summary>
        /// Navigation property for claims in this role.
        /// </summary>
        public virtual ICollection<IdentityRoleClaim<TKey>> Claims { get; } = (ICollection<IdentityRoleClaim<TKey>>)new List<IdentityRoleClaim<TKey>>();
        */
    }
}

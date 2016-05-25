using System;
using System.Collections.Generic;

namespace Tsc.Application.Repositories
{
    public class InMemoryDatabase : IDisposable
    {
        private class TableIds
        {
            public const string Roles = "roles";
            public const string Users = "users";
            public const string UserRoles = "userRoles";
            public const string UserClaims = "userClaims";
            public const string UserLogins = "userLogins";
            public const string RoleClaims = "roleClaims";
        }

        private static readonly Dictionary<string, object> Storage;

        static InMemoryDatabase()
        {
            Storage = new Dictionary<string, object>();
        }

        public InMemoryTable<TItem> GetTable<TItem>(string tableId)
        {
            return InternalGetStore<TItem>(tableId);
        }

        public InMemoryTable<TItem> RolesTable<TItem>()
        {
            return InternalGetStore<TItem>(TableIds.Roles);
        }

        public InMemoryTable<TItem> UsersTable<TItem>()
        {
            return InternalGetStore<TItem>(TableIds.Users);
        }

        public InMemoryTable<TItem> UserRolesTable<TItem>()
        {
            return InternalGetStore<TItem>(TableIds.UserRoles);
        }

        public InMemoryTable<TItem> UserClaimsTable<TItem>()
        {
            return InternalGetStore<TItem>(TableIds.UserClaims);
        }

        public InMemoryTable<TItem> UserLoginsTable<TItem>()
        {
            return InternalGetStore<TItem>(TableIds.UserLogins);
        }

        public InMemoryTable<TItem> RoleClaimsTable<TItem>()
        {
            return InternalGetStore<TItem>(TableIds.RoleClaims);
        }

        private InMemoryTable<TItem> InternalGetStore<TItem>(string tableId)
        {
            InMemoryTable<TItem> store;

            object rawStore;
            if (Storage.TryGetValue(tableId, out rawStore))
            {
                store = (InMemoryTable<TItem>) rawStore;
            }
            else
            {
                store = new InMemoryTable<TItem>();
                Storage.Add(tableId, store);
            }

            return store;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
        }

        #endregion
    }

    public class InMemoryTable<TItem> : List<TItem>
    {
        public void Update(TItem item, Predicate<TItem> selector)
        {
            var index = FindIndex(selector);
            if (index >= 0)
            {
                this[index] = item;
            }
        }
    }
}


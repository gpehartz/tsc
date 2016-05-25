using System;
using Tsc.Identity;

namespace Tsc.WebApi.ServiceModel
{
    public class User : IdentityUser<Guid>
    {
        public DateTime CreationDate { get; set; }
    }
}

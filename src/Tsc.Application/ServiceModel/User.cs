using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Tsc.Application.ServiceModel
{
    public class User : IdentityUser<Guid>
    {
        public DateTime CreationDate { get; set; }
    }
}

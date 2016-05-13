using System;

namespace Tsc.Application.ServiceModel
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }
    }
}

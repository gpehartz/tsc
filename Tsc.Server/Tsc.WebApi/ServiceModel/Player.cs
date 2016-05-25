using System;

namespace Tsc.WebApi.ServiceModel
{
    public class Player
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
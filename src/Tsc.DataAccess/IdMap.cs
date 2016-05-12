using System;
using Newtonsoft.Json;

namespace Tsc.DataAccess
{
    public class IdMap
    {
        [JsonProperty(PropertyName = "_id")]
        public string TechnicalId { get; set; }

        public Guid Id { get; set; }
    }
}
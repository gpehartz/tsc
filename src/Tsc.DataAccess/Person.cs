using System;
using Newtonsoft.Json;

namespace Tsc.DataAccess
{
    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }


        //public string _id { get; private set; }
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }
    }
}
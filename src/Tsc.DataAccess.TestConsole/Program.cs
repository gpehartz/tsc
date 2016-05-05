using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tsc.DataAccess.TestConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("helló");

            var dataAccess = new Tsc.DataAccess.MongoRestTscDataAccess(@"http://localhost:3000/");

            //team insert
            //dataAccess.Save(new Tsc.Domain.Team("Harapósok", new List<Tsc.Domain.User>
            //{
            //    new Tsc.Domain.User(Guid.NewGuid(), "Drakula"),
            //    new Tsc.Domain.User(Guid.NewGuid(), "Rancor"),
            //}));

            //var result = dataAccess.GetTeamById(Guid.Parse("6d89ee93-d4c4-43a0-b722-b792a847ec9d"));

            //var result = dataAccess.GetAllTeams();

            //team update
            var result = dataAccess.GetAllTeams();
            var team = result.First();
            dataAccess.Save(team);


            //dataAccess.Save(new Person {Name = "asdf", Age = 37});

            //var result = dataAccess.GetAllPersons();

            Console.ReadLine();
        }
    }
}

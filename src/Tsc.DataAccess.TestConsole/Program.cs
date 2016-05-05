using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsc.Domain;

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
            //var result = dataAccess.GetAllTeams();
            //var team = result.First();
            //dataAccess.Save(team);
            
            //dataAccess.Save(new Tournament("Kettes bajnokság", new List<Team>()));
            var result = dataAccess.GetAllTournaments();

            var tournament = result.First();
            tournament.SetFixtureResult(Guid.Parse("6b390253-1671-4549-88ca-5da10234cb46"), new[]
            {
                new MatchResult {HomeGoals = 10, AwayGoals = 0}
            });


            dataAccess.Save(tournament);
            
            Console.ReadLine();
        }
    }
}

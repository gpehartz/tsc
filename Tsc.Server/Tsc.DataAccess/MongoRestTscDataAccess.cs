using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Tsc.Domain;

namespace Tsc.DataAccess
{
    public class MongoRestTscDataAccess : MongoRestTscDataAccessBase, ITscDataAccess
    {
        private static class UrlPart
        {
            public const string Teams = @"tsc/teams";
            public const string Tournaments = @"tsc/tournaments";
        }

        public MongoRestTscDataAccess(IOptions<MongoRestTscDataAccessConfiguration> options)
            : base(options)
        {
        }

        internal MongoRestTscDataAccess(MongoRestTscDataAccessConfiguration configuration) 
            : base(configuration)
        {
        }

        #region Team related implementations

        public IEnumerable<Team> GetAllTeams()
        {
            return GetItems<Team>(UrlPart.Teams);
        }

        public Team GetTeam(Guid id)
        {
            return GetItem<Team>(id, UrlPart.Teams);
        }

        public void Save(Team team)
        {
            SaveItem(team, UrlPart.Teams);
        }

        #endregion Team related implementations
        #region Tournament related implementations

        public IEnumerable<Tournament> GetAllTournaments()
        {
            return GetItems<Tournament>(UrlPart.Tournaments);
        }

        public Tournament GetTournament(Guid id)
        {
            return GetItem<Tournament>(id, UrlPart.Tournaments);
        }

        public void Save(Tournament tournament)
        {
            SaveItem(tournament, UrlPart.Tournaments);
        }

        #endregion Tournament related implementations
    }
}
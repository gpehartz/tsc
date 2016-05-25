using System;
using System.Collections.Generic;
using Tsc.Domain;
using Tsc.Application.Definition;
using Tsc.DataAccess;

namespace Tsc.Application.Repositories
{
    public class PersistentTournamentRepository : ITournamentRepository
    {
        private readonly ITscDataAccess _dataAccess;

        public PersistentTournamentRepository(ITscDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public void Save(Tournament tournament)
        {
            _dataAccess.Save(tournament);
        }

        public IEnumerable<Tournament> GetAllTournaments()
        {
            return _dataAccess.GetAllTournaments();
        }

        public Tournament GetTournament(Guid id)
        {
            return _dataAccess.GetTournament(id);
        }
    }
}
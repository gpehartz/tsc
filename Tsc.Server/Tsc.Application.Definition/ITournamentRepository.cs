using System;
using System.Collections.Generic;
using Tsc.Domain;

namespace Tsc.Application.Definition
{
    public interface ITournamentRepository
    {
        void Save(Tournament tournament);
        IEnumerable<Tournament> GetAllTournaments();
        Tournament GetTournament(Guid id);
    }
}

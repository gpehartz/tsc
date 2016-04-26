﻿using System.Collections.Generic;

namespace Tsc.Domain.ExternalServices
{
    public interface ITournamentRepository
    {
        void Save(Tournament tournament);
        IEnumerable<Tournament> GetAllTournaments();
    }
}

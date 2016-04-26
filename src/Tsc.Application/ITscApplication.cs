﻿using System.Collections.Generic;
using Tsc.Application.ServiceModel;

namespace Tsc.Application
{
    public interface ITscApplication
    {
        void AddTournament(Tournament tournament);

        IEnumerable<Tournament> GetAllTournaments();

        IEnumerable<Team> GetAllTeams();
    }
}
using System;
using System.Collections.Generic;
using Tsc.Domain;
using Tsc.Domain.ExternalServices;

namespace Tsc.Application.Services
{
    public class TournamentRepository : ITournamentRepository
    {
        private static List<Tournament> _tournaments = new List<Tournament>
                           {
                               new Tournament("Egyes bajnokság"),
                               new Tournament("Valami más bajnokság"),
                               new Tournament("Ez meg egy harmadik bajnokság")
                           };

        public TournamentRepository()
        {
        }

        public IEnumerable<Tournament> GetAllTournaments()
        {
            return _tournaments;
        }

        public void Save(Tournament tournament)
        {
            _tournaments.Add(tournament);
        }
    }
}

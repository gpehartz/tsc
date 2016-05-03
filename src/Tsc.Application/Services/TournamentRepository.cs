using System;
using System.Collections.Generic;
using System.Linq;
using Tsc.Domain;
using Tsc.Domain.ExternalServices;

namespace Tsc.Application.Services
{
    public class TournamentRepository : ITournamentRepository
    {
        private static List<Tournament> _tournaments = new List<Tournament>
                           {
                               new Tournament("Egyes bajnokság", new List<Team>()),
                               new Tournament("Valami más bajnokság", new List<Team>()),
                               new Tournament("Ez meg egy harmadik bajnokság", new List<Team>())
                           };

        public TournamentRepository()
        {
        }

        public IEnumerable<Tournament> GetAllTournaments()
        {
            return _tournaments;
        }

        public Tournament GetTournament(Guid id)
        {
            return _tournaments.First(item => item.Id == id);
        }

        public void Save(Tournament tournament)
        {
            _tournaments.Add(tournament);
        }
    }
}

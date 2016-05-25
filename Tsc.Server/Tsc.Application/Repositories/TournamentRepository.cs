using System;
using System.Collections.Generic;
using System.Linq;
using Tsc.Domain;
using Tsc.Application.Definition;

namespace Tsc.Application.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        // ReSharper disable once InconsistentNaming
        private static readonly List<Tournament> _tournaments =
            new List<Tournament>
            {
                new Tournament("Egyes bajnokság", new List<Team>(), ""),
                new Tournament("Valami más bajnokság", new List<Team>(), ""),
                new Tournament("Ez meg egy harmadik bajnokság", new List<Team>(), "")
            };

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
            if (_tournaments.Any(item => item.Id == tournament.Id))
            {
                var localTournament = _tournaments.First(item => item.Id == tournament.Id);

                _tournaments.Remove(localTournament);
                _tournaments.Add(tournament);
            }
            else
            {
                _tournaments.Add(tournament);
            }        
        }
    }
}

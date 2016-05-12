using System;
using System.Collections.Generic;

namespace Tsc.Application.ServiceModel
{
    public class Tournament
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public List<Team> Participants { get; set; }

        public List<TournamentResultItem> Table { get; set; }

        public List<Round> Rounds { get; set; }

        public string LogoUrl { get; set; }
    }
}

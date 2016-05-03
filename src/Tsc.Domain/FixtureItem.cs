using System;

namespace Tsc.Domain
{
    public class FixtureItem
    {
        public FixtureItem(Team homeTeam, Team awayTeam)
        {
            Id = Guid.NewGuid();

            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
        }

        public Guid Id { get; private set; }

        public Team HomeTeam { get; private set; }

        public Team AwayTeam { get; private set; }

        public bool IsTeamInFixture(Team teamToCheck)
        {
            return HomeTeam.Id == teamToCheck.Id || AwayTeam.Id == teamToCheck.Id;
        }
    }
}

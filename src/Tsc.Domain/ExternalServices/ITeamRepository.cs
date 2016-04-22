using System.Collections.Generic;

namespace Tsc.Domain.ExternalServices
{
    public interface ITeamRepository
    {
        List<Team> GetAllTeams();
    }
}

using System.Collections.Generic;
using Tsc.Domain;

namespace Tsc.Application.Definition
{
    public interface IPlayerRepository
    {
        void Save(Player player);
        IEnumerable<Player> GetAllPlayers();
    }
}

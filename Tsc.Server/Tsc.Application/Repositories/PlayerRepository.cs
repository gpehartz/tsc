using System.Collections.Generic;
using Tsc.Domain;
using Tsc.Application.Definition;

namespace Tsc.Application.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private static List<Player> _players =
            new List<Player>
            {
                new Player("King_Geedorah87"),
                new Player("MrLions"),
                new Player("wayne_kenoff"),
                new Player("IMadeYouReadThis"),
                new Player("theSodommizer"),
                new Player("Thrubeingcool13"),
                new Player("lol_haha_dead"),
                new Player("Omni-Slash")
            };

        public IEnumerable<Player> GetAllPlayers()
        {
            return _players;
        }

        public void Save(Player player)
        {
            _players.Add(player);
        }
    }
}

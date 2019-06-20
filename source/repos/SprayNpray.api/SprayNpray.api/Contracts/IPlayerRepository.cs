using SprayNpray.api.Entities.ExtendedModels;
using SprayNpray.api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprayNpray.api.Contracts
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetAllPlayers();
        Player GetPlayerById(Guid playerId);
        PlayerExtended GetPlayerWithDetails(Guid playerId);
        void CreatePlayer(Player player);
        void UpdatePlayer(Player dbPlayer, Player player);
        void DeletePlayer(Player player);
        Player AuthenticatePlayer(Player player);
        Player GetPlayerByEmail(Player player);
    }
}

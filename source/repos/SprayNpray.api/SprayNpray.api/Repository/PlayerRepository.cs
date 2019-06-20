using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SprayNpray.api.Contracts;
using SprayNpray.api.Entities;
using SprayNpray.api.Entities.ExtendedModels;
using SprayNpray.api.Entities.Extensions;
using SprayNpray.api.Entities.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SprayNpray.api.Repository
{
    public class PlayerRepository : RepositoryBase<Player>, IPlayerRepository
    {
        
        public PlayerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
       
        public IEnumerable<Player> GetAllPlayers()
        {
            return FindAll()
                .OrderBy(pl => pl.Nickname)
                .ToList();
        }
        public Player GetPlayerById(Guid playerId)
        {
            return FindByCondition(player => player.Id.Equals(playerId))
                    .DefaultIfEmpty(new Player())
                    .FirstOrDefault();
        }

        public Player GetPlayerByEmail(Player player)
        {
            return FindByCondition(p => p.Email.Equals(player.Email))
                    .DefaultIfEmpty(new Player())
                    .FirstOrDefault();
        }

        public Player AuthenticatePlayer(Player playerDetails)
        {
            var player = GetPlayerByEmail(playerDetails);
            if(!BCrypt.Net.BCrypt.Verify(playerDetails.Password, player.Password))
            {
                return null;
            }

            player.Password = null;
            return player;
        }
        public PlayerExtended GetPlayerWithDetails(Guid playerId)
        {
            return new PlayerExtended(GetPlayerById(playerId))
            {
                Scores = RepositoryContext.Scores
                    .Where(s => s.Player_PlayerId == playerId)
            };
        }

        public void CreatePlayer(Player player)
        {
            player.Id = Guid.NewGuid();
            player.Password = BCrypt.Net.BCrypt.HashPassword(player.Password);
            Create(player);
        }
        public void UpdatePlayer(Player dbPlayer, Player player)
        {
            dbPlayer.Map(player);
            Update(dbPlayer);
        }
        public void DeletePlayer(Player player)
        {
            Delete(player);
        }

        
       
    }
}

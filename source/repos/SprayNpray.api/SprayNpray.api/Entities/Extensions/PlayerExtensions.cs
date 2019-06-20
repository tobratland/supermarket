using SprayNpray.api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprayNpray.api.Entities.Extensions
{
    public static class PlayerExtensions
    {
        public static void Map(this Player dbPlayer, Player player)
        {
            dbPlayer.Nickname = player.Nickname;
            dbPlayer.Password = player.Password;
            dbPlayer.Email = player.Email;
        }
    }
}

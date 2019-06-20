using SprayNpray.api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprayNpray.api.Entities.ExtendedModels
{
    public class PlayerExtended: IEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public IEnumerable<Score> Scores { get; set; }

        public PlayerExtended()
        {
        }

        public PlayerExtended(Player player)
        {
            Id = player.Id;
            Nickname = player.Nickname;
            Email = player.Email;
            Password = player.Password;
        }
        
    }
    
}

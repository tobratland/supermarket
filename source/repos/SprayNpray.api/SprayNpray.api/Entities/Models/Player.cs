using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SprayNpray.api.Entities.Models
{
    [Table("players")]
    public class Player: IEntity
    {
        [Key]
        [Column("PlayerId")]
        public Guid Id { get; set; }

        
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters")]
        public string Email { get; set; }

        
        [StringLength(45, ErrorMessage = "Nickname cant be longer than 45 characters")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        
    }
}
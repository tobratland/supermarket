using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SprayNpray.api.Entities.Models
{
    [Table("scores")]
    public class Score: IEntity
    {
        [Key]
        [Column("ScoreId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Weapon is required")]
        public string Weapon { get; set; }

        [Required(ErrorMessage = "Date created is required")]
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "Time is required")]
        public int Time { get; set; }

        [Required(ErrorMessage = "Score is required")]
        public int TheScore { get; set; }

        [Required(ErrorMessage = "Number of bad reloads is required")]
        public int BadReloads { get; set; }

        [Required(ErrorMessage = "Number of good reloads is required")]
        public int GoodReloads { get; set; }

        [Required(ErrorMessage = "Missrate is required")]
        public decimal Missrate { get; set; }

        [Required(ErrorMessage = "type of magazine required")]
        public int MagazineExtension { get; set; }

        [Required(ErrorMessage = "Type of barrel required")]
        public int BarrelExtension { get; set; }

        [Required(ErrorMessage = "PlayerId is required")]
        public Guid Player_PlayerId { get; set; }

    }
}

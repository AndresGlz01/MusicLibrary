using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicLibrary.Models
{
    public partial class Artistum
    {
        public Artistum()
        {
            Albums = new HashSet<Album>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Nacionalidad { get; set; } = null!;

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        public int IdGenero { get; set; }

        [Display(Name = "Género")]
        public virtual Genero IdGeneroNavigation { get; set; } = null!;
        public virtual ICollection<Album> Albums { get; set; }
    }
}

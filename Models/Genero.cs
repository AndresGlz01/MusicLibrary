using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicLibrary.Models
{
    public partial class Genero
    {
        public Genero()
        {
            Artista = new HashSet<Artistum>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre")]
        [MinLength(3, ErrorMessage = "Ingrese un nombre más largo")]
        [MaxLength(100, ErrorMessage = "Ingrese un nombre más corto")]
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Artistum> Artista { get; set; }
    }
}

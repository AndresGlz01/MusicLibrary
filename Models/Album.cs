using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicLibrary.Models
{
    public partial class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdArtista { get; set; }
        public string Nombre { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime FechaLanzamiento { get; set; }

        public virtual Artistum IdArtistaNavigation { get; set; } = null!;
    }
}

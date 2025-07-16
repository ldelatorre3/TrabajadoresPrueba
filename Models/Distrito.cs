using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajadoresWeb.Models
{
    [Table("Distrito")]
    public class Distrito
    {
        [Key]
        public int Id { get; set; }

        public int? IdProvincia { get; set; }

        [MaxLength(500)]
        public string NombreDistrito { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajadoresPrueba.Models
{
    [Table("Provincia")]
    public class Provincia
    {
        [Key]
        public int Id { get; set; }

        public int? IdDepartamento { get; set; }

        [MaxLength(500)]
        public string NombreProvincia { get; set; }
    }
}
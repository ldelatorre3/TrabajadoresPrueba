using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajadoresPrueba.Models
{
    [Table("Departamento")]
    public class Departamento
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        public string NombreDepartamento { get; set; }
    }
}
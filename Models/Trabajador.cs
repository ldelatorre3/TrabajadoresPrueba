using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajadoresWeb.Models
{
    [Table("Trabajadores")]
    public class Trabajador
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(3)]
        public string TipoDocumento { get; set; }

        [MaxLength(50)]
        public string NumeroDocumento { get; set; }

        [MaxLength(500)]
        public string Nombres { get; set; }

        [MaxLength(1)]
        public string Sexo { get; set; }

        [Column("IdDepartamento")]
        public int? DepartamentoId { get; set; }

        [Column("IdProvincia")]
        public int? ProvinciaId { get; set; }

        [Column("IdDistrito")]
        public int? DistritoId { get; set; }

        // Relaciones (opcional por ahora)
        public Departamento? Departamento { get; set; }
        public Provincia? Provincia { get; set; }
        public Distrito? Distrito { get; set; }
    }
}
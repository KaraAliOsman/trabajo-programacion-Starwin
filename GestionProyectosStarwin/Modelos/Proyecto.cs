using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionProyectosStarwin.Modelos
{
    public class Proyecto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]


        [MaxLength(150)]
        public string NombreCliente { get; set; }
        [Required(ErrorMessage = "La descripción del proyecto es obligatoria.")]
        public string DescripcionProyecto { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0, double.MaxValue, ErrorMessage = "El presupuesto no puede ser negativo.")]
        public decimal PresupuestoTotal { get; set; }
        public DateTime FechaEntregaEstimada { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public string Estado { get; set; }
    }
}
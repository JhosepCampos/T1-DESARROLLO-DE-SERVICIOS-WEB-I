using System.ComponentModel.DataAnnotations;

namespace T1CAMPOSCRUZJHOSEPANTONY.Models
{
    public class PrestamoAnioViewModel
    {
        [Display(Name = "ID Préstamo")]
        public int IdPrestamo { get; set; }

        [Display(Name = "Fecha Préstamo")]
        [DataType(DataType.Date)]
        public DateTime FechaPrestamo { get; set; }

        [Display(Name = "Fecha Devolución")]
        [DataType(DataType.Date)]
        public DateTime? FechaDevolucion { get; set; }

        [Display(Name = "Estudiante")]
        public string ApellidosNombres { get; set; } = string.Empty;

        [Display(Name = "Libro")]
        public string Titulo { get; set; } = string.Empty;

        [Display(Name = "Estado")]
        public string EstadoPrestamo { get; set; } = string.Empty;

        [Display(Name = "Multa (S/)")]
        public decimal Multa { get; set; }
    }
}

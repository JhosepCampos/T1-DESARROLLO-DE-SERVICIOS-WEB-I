using System.ComponentModel.DataAnnotations;

namespace T1CAMPOSCRUZJHOSEPANTONY.Models
{
    public class CategoriaLibroViewModel
    {

        [Required(ErrorMessage = "El código de categoría es obligatorio.")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "El código debe tener exactamente 5 caracteres.")]
        [Display(Name = "Código")]
        public string CodCategoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(60, ErrorMessage = "El nombre no puede superar los 60 caracteres.")]
        [Display(Name = "Categoría")]
        public string NombreCategoria { get; set; } = string.Empty;

        [StringLength(150, ErrorMessage = "La descripción no puede superar los 150 caracteres.")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

    }
}

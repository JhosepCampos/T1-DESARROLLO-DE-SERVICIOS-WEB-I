using System.ComponentModel.DataAnnotations;

namespace T1CAMPOSCRUZJHOSEPANTONY.Models
{
    public class LibroViewModel
    {
        [Required(ErrorMessage = "El código del libro es obligatorio.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "El código debe tener exactamente 6 caracteres (ej. LIB001).")]
        [Display(Name = "Código de Libro")]
        public string CodLibro { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        [Display(Name = "Categoría")]
        public string CodCategoria { get; set; } = string.Empty;
       
        [Display(Name = "Categoría")]
        public string NombreCategoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(120, ErrorMessage = "El título no puede superar los 120 caracteres.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ISBN es obligatorio.")]
        [StringLength(20, ErrorMessage = "El ISBN no puede superar los 20 caracteres.")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "El año de publicación es obligatorio.")]
        [Range(1450, 2100, ErrorMessage = "El año debe estar entre 1450 y 2100.")]
        [Display(Name = "Año de Publicación")]
        public int AnioPublicacion { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [Range(0, 1, ErrorMessage = "Estado inválido.")]
        public byte Estado { get; set; } = 1;
    }
}

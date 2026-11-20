using T1CAMPOSCRUZJHOSEPANTONY.Models;

namespace T1CAMPOSCRUZJHOSEPANTONY.Service
{
    public interface ILibroService
    {
        Task<List<LibroViewModel>> ListarLibrosAsync();
        Task<LibroViewModel?> ObtenerLibroPorIdAsync(string codLibro);
        Task<bool> InsertarLibroAsync(LibroViewModel libro);
        Task<bool> ActualizarLibroAsync(LibroViewModel libro);
        Task<bool> EliminarLibroAsync(string codLibro);
        Task<List<CategoriaLibroViewModel>> ListarCategoriasAsync();
        Task<List<PrestamoAnioViewModel>> ListarPrestamosPorAnioAsync(int anio);

    }
}

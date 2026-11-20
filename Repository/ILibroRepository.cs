using T1CAMPOSCRUZJHOSEPANTONY.Entity;

namespace T1CAMPOSCRUZJHOSEPANTONY.Repository
{
    public interface ILibroRepository
    {
        Task<List<LibroEntity>> ListarLibroAsync();
        Task<LibroEntity?> ObtenerLibroPorIdAsync(string codLibro);
        Task<bool> InsertarLibroAsync(LibroEntity libro);
        Task<bool> ActualizarLibroAsync(LibroEntity libro);
        Task<bool> EliminarLibroAsync(string codLibro);
        Task<List<CategoriaLibroEntity>> ListarCategoriasAsync();
        Task<List<PrestamoAnioEntity>> ListarPrestamosPorAnioAsync(int anio);


    }
}

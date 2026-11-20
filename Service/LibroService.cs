using Microsoft.Data.SqlClient;
using T1CAMPOSCRUZJHOSEPANTONY.Entity;
using T1CAMPOSCRUZJHOSEPANTONY.Models;
using T1CAMPOSCRUZJHOSEPANTONY.Repository;

namespace T1CAMPOSCRUZJHOSEPANTONY.Service
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository _libroRepository;

        public LibroService(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }

        public async Task<List<LibroViewModel>> ListarLibrosAsync()
        {
            var librosBD = await _libroRepository.ListarLibroAsync();

            return librosBD.Select(l => new LibroViewModel
            {
                CodLibro = l.CodLibro,
                CodCategoria = l.CodCategoria,
                NombreCategoria = l.NombreCategoria,
                Titulo = l.Titulo,
                ISBN = l.ISBN,
                AnioPublicacion = l.AnioPublicacion,
                Stock = l.Stock,
                Estado = l.Estado
            }).ToList();
        }

        public async Task<bool> InsertarLibroAsync(LibroViewModel model)
        {
            // Reglas de negocio según requisitos
            if (string.IsNullOrWhiteSpace(model.CodLibro) || model.CodLibro.Trim().Length != 6)
                throw new Exception("El código del libro debe tener exactamente 6 caracteres.");

            if (model.AnioPublicacion < 1450 || model.AnioPublicacion > 2100)
                throw new Exception("El año de publicación debe estar entre 1450 y 2100.");

            if (model.Stock < 0)
                throw new Exception("El stock no puede ser negativo.");

            var entidad = new LibroEntity
            {
                CodLibro = model.CodLibro.Trim().ToUpper(),
                CodCategoria = model.CodCategoria.Trim(),
                Titulo = model.Titulo.Trim(),
                ISBN = model.ISBN.Trim(),
                AnioPublicacion = model.AnioPublicacion,
                Stock = model.Stock,
                Estado = model.Estado
            };

            try
            {
                return await _libroRepository.InsertarLibroAsync(entidad);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new Exception("Ya existe un libro registrado con ese código o ISBN.");
            }
        }

        public async Task<bool> ActualizarLibroAsync(LibroViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.CodLibro))
                throw new Exception("El código del libro no es válido.");

            if (model.AnioPublicacion < 1450 || model.AnioPublicacion > 2100)
                throw new Exception("El año de publicación debe estar entre 1450 y 2100.");

            if (model.Stock < 0)
                throw new Exception("El stock no puede ser negativo.");

            var entidad = new LibroEntity
            {
                CodLibro = model.CodLibro.Trim(),
                CodCategoria = model.CodCategoria.Trim(),
                Titulo = model.Titulo.Trim(),
                ISBN = model.ISBN.Trim(),
                AnioPublicacion = model.AnioPublicacion,
                Stock = model.Stock,
                Estado = model.Estado
            };

            try
            {
                return await _libroRepository.ActualizarLibroAsync(entidad);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new Exception("El ISBN ingresado ya pertenece a otro libro.");
            }
        }

        public async Task<bool> EliminarLibroAsync(string codLibro)
        {
            if (string.IsNullOrWhiteSpace(codLibro)) return false;

            try
            {
                return await _libroRepository.EliminarLibroAsync(codLibro.Trim());
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new Exception("No se puede eliminar el libro porque tiene préstamos relacionados.");
            }
        }

        public async Task<List<CategoriaLibroViewModel>> ListarCategoriasAsync()
        {
            var categoriasBD = await _libroRepository.ListarCategoriasAsync();

            return categoriasBD.Select(c => new CategoriaLibroViewModel
            {
                CodCategoria = c.CodCategoria,
                NombreCategoria = c.NombreCategoria
            }).ToList();
        }

        public async Task<LibroViewModel?> ObtenerLibroPorIdAsync(string codLibro)
        {
            if (string.IsNullOrWhiteSpace(codLibro)) return null;

            var libro = await _libroRepository.ObtenerLibroPorIdAsync(codLibro.Trim());
            if (libro == null) return null;

            return new LibroViewModel
            {
                CodLibro = libro.CodLibro,
                CodCategoria = libro.CodCategoria,
                NombreCategoria = libro.NombreCategoria,
                Titulo = libro.Titulo,
                ISBN = libro.ISBN,
                AnioPublicacion = libro.AnioPublicacion,
                Stock = libro.Stock,
                Estado = libro.Estado
            };
        }

        public async Task<List<PrestamoAnioViewModel>> ListarPrestamosPorAnioAsync(int anio)
        {
            var entidades = await _libroRepository.ListarPrestamosPorAnioAsync(anio);

            return entidades.Select(p => new PrestamoAnioViewModel
            {
                IdPrestamo = p.IdPrestamo,
                FechaPrestamo = p.FechaPrestamo,
                FechaDevolucion = p.FechaDevolucion,
                ApellidosNombres = p.ApellidosNombres,
                Titulo = p.Titulo,
                EstadoPrestamo = p.EstadoPrestamo,
                Multa = p.Multa
            }).ToList();
        }
    }
}

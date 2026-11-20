using Microsoft.Data.SqlClient;
using System.Data;
using T1CAMPOSCRUZJHOSEPANTONY.Data;
using T1CAMPOSCRUZJHOSEPANTONY.Entity;

namespace T1CAMPOSCRUZJHOSEPANTONY.Repository
{
    public class LibroRepository : ILibroRepository
    {
        private readonly ConexionBD _conexion;

        public LibroRepository(ConexionBD conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<LibroEntity>> ListarLibroAsync()
        {
            var libros = new List<LibroEntity>();

            using (SqlConnection ctx = _conexion.ObtenerConexion())
            {
                await ctx.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("USP_LIBROS_LISTAR", ctx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            libros.Add(new LibroEntity
                            {
                                CodLibro = (Convert.ToString(reader["CodLibro"]) ?? string.Empty).Trim(),
                                CodCategoria = (Convert.ToString(reader["CodCategoria"]) ?? string.Empty).Trim(),
                                NombreCategoria = Convert.ToString(reader["NombreCategoria"]) ?? string.Empty,
                                Titulo = Convert.ToString(reader["Titulo"]) ?? string.Empty,
                                ISBN = Convert.ToString(reader["ISBN"]) ?? string.Empty,
                                AnioPublicacion = Convert.ToInt32(reader["AnioPublicacion"]),
                                Stock = Convert.ToInt32(reader["Stock"]),
                                Estado = Convert.ToByte(reader["Estado"])
                            });
                        }
                    }
                }
            }

            return libros;
        }

        public async Task<bool> InsertarLibroAsync(LibroEntity libro)
        {
            using (SqlConnection ctx = _conexion.ObtenerConexion())
            {
                await ctx.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("USP_LIBROS_INSERTAR", ctx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodLibro", libro.CodLibro.Trim());
                    cmd.Parameters.AddWithValue("@CodCategoria", libro.CodCategoria.Trim());
                    cmd.Parameters.AddWithValue("@Titulo", libro.Titulo.Trim());
                    cmd.Parameters.AddWithValue("@ISBN", libro.ISBN.Trim());
                    cmd.Parameters.AddWithValue("@AnioPublicacion", libro.AnioPublicacion);
                    cmd.Parameters.AddWithValue("@Stock", libro.Stock);
                    cmd.Parameters.AddWithValue("@Estado", libro.Estado);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
        }

        public async Task<bool> ActualizarLibroAsync(LibroEntity libro)
        {
            using (SqlConnection ctx = _conexion.ObtenerConexion())
            {
                await ctx.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("USP_LIBROS_ACTUALIZAR", ctx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodLibro", libro.CodLibro.Trim());
                    cmd.Parameters.AddWithValue("@CodCategoria", libro.CodCategoria.Trim());
                    cmd.Parameters.AddWithValue("@Titulo", libro.Titulo.Trim());
                    cmd.Parameters.AddWithValue("@ISBN", libro.ISBN.Trim());
                    cmd.Parameters.AddWithValue("@AnioPublicacion", libro.AnioPublicacion);
                    cmd.Parameters.AddWithValue("@Stock", libro.Stock);
                    cmd.Parameters.AddWithValue("@Estado", libro.Estado);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
        }

        public async Task<bool> EliminarLibroAsync(string codLibro)
        {
            using (SqlConnection ctx = _conexion.ObtenerConexion())
            {
                await ctx.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("USP_LIBROS_ELIMINAR", ctx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodLibro", codLibro.Trim());

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
        }

        public async Task<List<CategoriaLibroEntity>> ListarCategoriasAsync()
        {
            var categorias = new List<CategoriaLibroEntity>();

            using (SqlConnection ctx = _conexion.ObtenerConexion())
            {
                await ctx.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("USP_CATEGORIAS_LISTAR", ctx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            categorias.Add(new CategoriaLibroEntity
                            {
                                CodCategoria = (Convert.ToString(reader["CodCategoria"]) ?? string.Empty).Trim(),
                                NombreCategoria = Convert.ToString(reader["NombreCategoria"]) ?? string.Empty
                            });
                        }
                    }
                }
            }

            return categorias;
        }

        public async Task<LibroEntity?> ObtenerLibroPorIdAsync(string codLibro)
        {
            LibroEntity? result = null;

            using (SqlConnection ctx = _conexion.ObtenerConexion())
            {
                await ctx.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("USP_LIBROS_BUSCAR", ctx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodLibro", codLibro);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            result = new LibroEntity
                            {
                                CodLibro = (Convert.ToString(reader["CodLibro"]) ?? string.Empty).Trim(),
                                CodCategoria = (Convert.ToString(reader["CodCategoria"]) ?? string.Empty).Trim(),
                                NombreCategoria = Convert.ToString(reader["NombreCategoria"]) ?? string.Empty,
                                Titulo = Convert.ToString(reader["Titulo"]) ?? string.Empty,
                                ISBN = Convert.ToString(reader["ISBN"]) ?? string.Empty,
                                AnioPublicacion = Convert.ToInt32(reader["AnioPublicacion"]),
                                Stock = Convert.ToInt32(reader["Stock"]),
                                Estado = Convert.ToByte(reader["Estado"])
                            };
                        }
                    }
                }
            }

            return result;
        }

        public async Task<List<PrestamoAnioEntity>> ListarPrestamosPorAnioAsync(int anio)
        {
            var lista = new List<PrestamoAnioEntity>();

            using (SqlConnection ctx = _conexion.ObtenerConexion())
            {
                await ctx.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("USP_PRESTAMOSPORANIO", ctx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Anio", anio);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new PrestamoAnioEntity
                            {
                                IdPrestamo = Convert.ToInt32(reader["IdPrestamo"]),
                                FechaPrestamo = Convert.ToDateTime(reader["FechaPrestamo"]),
                                FechaDevolucion = reader["FechaDevolucion"] != DBNull.Value
                                                  ? Convert.ToDateTime(reader["FechaDevolucion"])
                                                  : null,
                                ApellidosNombres = Convert.ToString(reader["ApellidosNombres"]) ?? string.Empty,
                                Titulo = Convert.ToString(reader["Titulo"]) ?? string.Empty,
                                EstadoPrestamo = Convert.ToString(reader["EstadoPrestamo"]) ?? string.Empty,
                                Multa = Convert.ToDecimal(reader["Multa"])
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}

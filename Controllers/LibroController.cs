using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using T1CAMPOSCRUZJHOSEPANTONY.Models;
using T1CAMPOSCRUZJHOSEPANTONY.Service;

namespace T1CAMPOSCRUZJHOSEPANTONY.Controllers
{
    public class LibroController : Controller
    {
        private readonly ILibroService _iLibroService;

        public LibroController(ILibroService iLibroService)
        {
            _iLibroService = iLibroService;
        }

        private async Task CargarCategoriasAsync(string? categoriaSeleccionada = null)
        {
            try
            {
                var categorias = await _iLibroService.ListarCategoriasAsync();
                ViewBag.Categorias = new SelectList(categorias, "CodCategoria", "NombreCategoria", categoriaSeleccionada);
            }
            catch (Exception)
            {
                ViewBag.Categorias = new SelectList(new List<CategoriaLibroViewModel>(), "CodCategoria", "NombreCategoria");
                ViewBag.Mensaje = "No hay conexión con la base de datos para cargar las categorías.";
            }
        }

        // GET: LibroController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            ViewData["Titulo"] = "Catálogo de libros";
            ViewBag.Mensaje = "Bienvenido a la biblioteca";

            try
            {
                var listaLibros = await _iLibroService.ListarLibrosAsync();
                return View(listaLibros);
            }
            catch (Exception)
            {
                TempData["Error"] = "No hay conexión con la base de datos en este momento.";
                return View(new List<LibroViewModel>());
            }
        }

        // GET: LibroController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return NotFound();

            LibroViewModel? libro = await _iLibroService.ObtenerLibroPorIdAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: LibroController/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await CargarCategoriasAsync();
            return View(new LibroViewModel());
        }

        // POST: LibroController/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LibroViewModel libro)
        {
            if (!ModelState.IsValid)
            {
                await CargarCategoriasAsync(libro.CodCategoria);
                return View(libro);
            }

            try
            {
                bool registrado = await _iLibroService.InsertarLibroAsync(libro);

                if (registrado)
                {
                    TempData["Exito"] = $"Libro '{libro.Titulo}' registrado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Mensaje = "Error al registrar el libro en la base de datos.";
                    await CargarCategoriasAsync(libro.CodCategoria);
                    return View(libro);
                }
            }
            catch (Exception ex)
            {
                // Atrapa errores de negocio (código/ISBN duplicado, año inválido, etc.)
                ViewBag.Mensaje = "Ocurrió un error: " + ex.Message;
                await CargarCategoriasAsync(libro.CodCategoria);
                return View(libro);
            }
        }


        // GET: LibroController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return NotFound();

            LibroViewModel? libro = await _iLibroService.ObtenerLibroPorIdAsync(id);

            if (libro == null) return NotFound();

            await CargarCategoriasAsync(libro.CodCategoria);
            return View(libro);
        }

        // POST: LibroController/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LibroViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                await CargarCategoriasAsync(modelo.CodCategoria);
                return View(modelo);
            }

            try
            {
                bool modificado = await _iLibroService.ActualizarLibroAsync(modelo);
                if (modificado)
                {
                    TempData["Exito"] = $"Libro '{modelo.Titulo}' actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Mensaje = "No se pudo actualizar el libro en la base de datos.";
                await CargarCategoriasAsync(modelo.CodCategoria);
                return View(modelo);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error: " + ex.Message;
                await CargarCategoriasAsync(modelo.CodCategoria);
                return View(modelo);
            }
        }

        // GET: LibroController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return NotFound();

            LibroViewModel? libro = await _iLibroService.ObtenerLibroPorIdAsync(id);

            if (libro == null) return NotFound();

            return View(libro);
        }

        // POST: LibroController/Delete/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, LibroViewModel model)
        {
            try
            {
                bool eliminado = await _iLibroService.EliminarLibroAsync(id);
                if (eliminado)
                {
                    TempData["Exito"] = "El libro fue eliminado correctamente.";
                }
                else
                {
                    TempData["Error"] = "No se pudo eliminar el libro.";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Aquí captura el mensaje claro si tiene préstamos relacionados
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IEnumerable<PrestamoAnioViewModel>> ListarPrestamosAnio(int anio)
        {
            return await _iLibroService.ListarPrestamosPorAnioAsync(anio);
        }

        [HttpGet]
        public async Task<IActionResult> PrestamosPorAnio(int? anio, int pagina = 1)
        {
            int anioConsulta = anio ?? DateTime.Now.Year;
            if (pagina < 1) pagina = 1;

            int registrosPorPagina = 10;

            try
            {
                var todosLosPrestamos = (await ListarPrestamosAnio(anioConsulta)).ToList();

                int totalRegistros = todosLosPrestamos.Count;
                int totalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPagina);

                var prestamosPaginados = todosLosPrestamos
                    .Skip((pagina - 1) * registrosPorPagina)
                    .Take(registrosPorPagina)
                    .ToList();

                ViewBag.AnioActual = anioConsulta;
                ViewBag.TotalRegistros = totalRegistros;
                ViewBag.PaginaActual = pagina;
                ViewBag.TotalPaginas = totalPaginas;

                return View(prestamosPaginados);
            }
            catch (Exception)
            {
                ViewBag.AnioActual = anioConsulta;
                ViewBag.TotalRegistros = 0;
                ViewBag.PaginaActual = 1;
                ViewBag.TotalPaginas = 0;
                ViewBag.ErrorConexion = "No hay conexión con la base de datos en este momento.";

                return View(new List<PrestamoAnioViewModel>());
            }
        }
    }
}

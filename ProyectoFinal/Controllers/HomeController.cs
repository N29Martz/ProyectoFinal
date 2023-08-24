using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Servicios;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace ProyectoFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositorioPlatillo repositorioPlatillo;
        private readonly IRepositorioPostres repositorioPostres;
        private readonly IRepositorioBebidas repositorioBebidas;

        public HomeController(ILogger<HomeController> logger, 
            IRepositorioPlatillo repositorioPlatillo,
            IRepositorioPostres repositorioPostres,
            IRepositorioBebidas repositorioBebidas)
        {
            _logger = logger;
            this.repositorioPlatillo = repositorioPlatillo;
            this.repositorioPostres = repositorioPostres;
            this.repositorioBebidas = repositorioBebidas;
        }

        public async Task<IActionResult> Index()
        {
            var platillo = await repositorioPlatillo.ObtenerDatos();
            return View(platillo);
        }

        [HttpGet]
        public async Task<IActionResult> EditarPlatillo(int id)
        {
            var platillo = await repositorioPlatillo.ObtenerPorId(id);

            if (platillo is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(platillo);
        }

        [HttpPost]
        public async Task<IActionResult> EditarPLatillo(Platillo modelo, IFormFile Imagen)
        {
            var pedidoExiste = await repositorioPlatillo.ObtenerPorId(modelo.Id);

            if (pedidoExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            if (Imagen != null && Imagen.Length > 0)
            {
                _logger.LogInformation("entro");
                using (var memoryStream = new MemoryStream())
                {
                    await Imagen.CopyToAsync(memoryStream);
                    modelo.Img = memoryStream.ToArray();
                }

            }

            await repositorioPlatillo.EditarPlatillo(modelo);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var platillo = await repositorioPlatillo.ObtenerPorId(id);

            if (platillo is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(platillo);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarPlatillo(int id)
        {
            var platilloExiste = await repositorioPlatillo.ObtenerPorId(id);

            if (platilloExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioPlatillo.EliminarPlatillo(id);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult NoEncontrado()
        {
            return View();
        }

        public IActionResult OrdenesenProceso()
        {
            return View();
        }

        public IActionResult Comentarios()
        {
            return View();
        }

        public async Task<IActionResult> IndexBebidas()
        {
            var bebida = await repositorioBebidas.ObtenerDatos();
            return View(bebida);
        }

        public async Task<IActionResult> IndexPostres()
        {
            var postre = await repositorioPostres.ObtenerDatos();
            return View(postre);
        }

        public IActionResult CrearPlatillo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearPlatillo(Platillo modelo, IFormFile Imagen)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            if (Imagen != null && Imagen.Length > 0)
            {
                _logger.LogInformation("entro");
                using (var memoryStream = new MemoryStream())
                {
                    await Imagen.CopyToAsync(memoryStream);
                    modelo.Img = memoryStream.ToArray();
                }

            }

            await repositorioPlatillo.CrearPlatillo(modelo);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CrearBebida()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CrearBebida(Bebida modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            await repositorioBebidas.CrearBebida(modelo);
            return RedirectToAction("IndexBebidas");

        }

        [HttpGet]

        public async Task<IActionResult> EditarBebida(int id)
        {
            var bebida = await repositorioBebidas.ObtenerPorId(id);

            if (bebida is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(bebida);
        }

        [HttpPost]

        public async Task<IActionResult> EditarBebida(Bebida modelo)
        {
            var pedidoExiste = await repositorioBebidas.ObtenerPorId(modelo.Id);

            if (pedidoExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioBebidas.EditarBebida(modelo);

            return RedirectToAction("IndexBebidas");
        }

        [HttpGet]
        public async Task<IActionResult> EliminarBebida(int id)
        {
            var bebida = await repositorioBebidas.ObtenerPorId(id);

            if (bebida is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(bebida);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarlaBebida(int id)
        {
            var pedidoExiste = await repositorioBebidas.ObtenerPorId(id);

            if (pedidoExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioBebidas.EliminarBebida(id);

            return RedirectToAction("IndexBebidas");
        }

        public IActionResult Postres()
        {
            return View();
        }

        public IActionResult CrearPostres()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearPostres(Postres modelo, IFormFile Imagen)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            if (Imagen != null && Imagen.Length > 0)
            {
                _logger.LogInformation("entro");
                using (var memoryStream = new MemoryStream())
                {
                    await Imagen.CopyToAsync(memoryStream);
                    modelo.Img = memoryStream.ToArray();
                }

            }

            await repositorioPostres.CrearPostre(modelo);

            return RedirectToAction("IndexPostres");
        }

        [HttpGet]
        public async Task<IActionResult> EditarPostre(int id)
        {
            var postre = await repositorioPostres.ObtenerPorId(id);

            if (postre is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(postre);
        }

        [HttpPost]
        public async Task<IActionResult> EditarPostre(Postres modelo, IFormFile Imagen)
        {
            var postreExiste = await repositorioPostres.ObtenerPorId(modelo.Id);

            if (postreExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            if (Imagen != null && Imagen.Length > 0)
            {
                _logger.LogInformation("entro");
                using (var memoryStream = new MemoryStream())
                {
                    await Imagen.CopyToAsync(memoryStream);
                    modelo.Img = memoryStream.ToArray();
                }

            }

            await repositorioPostres.EditarPostre(modelo);

            return RedirectToAction("IndexPostres");
        }

        [HttpGet]
        public async Task<IActionResult> EliminarPostre(int id)
        {
            var postre = await repositorioPostres.ObtenerPorId(id);

            if (postre is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(postre);
        }

        [HttpPost]
        public async Task<IActionResult> EliminaPostre(int id)
        {
            var postreExiste = await repositorioPostres.ObtenerPorId(id);

            if (postreExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioPostres.EliminaPostre(id);

            return RedirectToAction("Index");
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
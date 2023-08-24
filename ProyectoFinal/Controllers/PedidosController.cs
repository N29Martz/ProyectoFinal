using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Servicios;

namespace ProyectoFinal.Controllers
{
    public class PedidosController : Controller
    {
        
        private readonly IRepositorioPedidos repositorioPedidos;

        public PedidosController(
            IRepositorioPedidos repositorioPedidos)
        {
            this.repositorioPedidos = repositorioPedidos;
        }
        public async Task<IActionResult> Index()
        {
            var pedidos = await repositorioPedidos.ObtenerDatos();
            return View(pedidos);
        }

        [HttpGet]
        public IActionResult CrearPedido()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearPedido(Pedido modelo)
        {
            
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            await repositorioPedidos.CrearPedido(modelo);

            return RedirectToAction("Index");

        }

        [HttpGet]

        public async Task<IActionResult> EditarPedido(int id)
        {
            var pedido = await repositorioPedidos.ObtenerPorId(id);

            if (pedido is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(pedido);
        }

        [HttpPost]

        public async Task<IActionResult> EditarPedido(Pedido modelo)
        {
            var pedidoExiste = await repositorioPedidos.ObtenerPorId(modelo.Id);

            if (pedidoExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioPedidos.EditarPedido(modelo);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var pedido = await repositorioPedidos.ObtenerPorId(id);

            if (pedido is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarPedido(int id)
        {
            var pedidoExiste = await repositorioPedidos.ObtenerPorId(id);

            if (pedidoExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioPedidos.EliminarPedido(id);

            return RedirectToAction("Index");
        }




        [HttpPost]
        public async Task<IActionResult> MarcarEntregado(int id)
        {
            var pedido = await repositorioPedidos.ObtenerPorId(id);

            if (pedido is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioPedidos.MarcarEntregado(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AnularEntrega(int id)
        {

            var pedido = await repositorioPedidos.ObtenerPorId(id);

            if (pedido is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioPedidos.AnularEntrega(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BuscarPorNombre(string busqueda)
        {
            var pedidos = await repositorioPedidos.BuscarPorNombre(busqueda); 
            return View("ResultadoBusqueda", pedidos); // Mostrar los resultados en la vista de resultados de búsqueda
        }
    }

}

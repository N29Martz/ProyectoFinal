using ProyectoFinal.Models;

namespace ProyectoFinal.Servicios
{
    public interface IRepositorioPedidos
    {
        Task AnularEntrega(int id);
        Task<IEnumerable<Pedido>> BuscarPorNombre(string busqueda);
        Task CrearPedido(Pedido modelo);
        Task EditarPedido(Pedido modelo);
        Task EliminarPedido(int id);
        Task MarcarEntregado(int id);
        Task<IEnumerable<Pedido>> ObtenerDatos();
        Task<Pedido> ObtenerPorId(int id);
        //Task<Pedido> ObtenerPorId(int id);
    }
}
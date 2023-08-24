using ProyectoFinal.Models;

namespace ProyectoFinal.Servicios
{
    public interface IRepositorioBebidas
    {
        Task CrearBebida(Bebida modelo);
        Task EditarBebida(Bebida modelo);
        Task EliminarBebida(int id);
        Task<IEnumerable<Bebida>> ObtenerDatos();
        Task<Bebida> ObtenerPorId(int id);
    }
}
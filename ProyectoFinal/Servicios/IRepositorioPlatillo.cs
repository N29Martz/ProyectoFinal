using ProyectoFinal.Models;

namespace ProyectoFinal.Servicios
{
    public interface IRepositorioPlatillo
    {
        Task CrearPlatillo(Platillo modelo);
        Task EditarPlatillo(Platillo modelo);
        Task EliminarPlatillo(int id);
        Task<IEnumerable<Platillo>> ObtenerDatos();
        Task<Platillo> ObtenerPorId(int id);
    }
}
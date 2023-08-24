using ProyectoFinal.Models;

namespace ProyectoFinal.Servicios
{
    public interface IRepositorioPostres
    {
        Task CrearPostre(Postres modelo);
        Task EditarPostre(Postres modelo);
        Task EliminaPostre(int id);
        Task<IEnumerable<Postres>> ObtenerDatos();
        Task<Postres> ObtenerPorId(int id);
    }
}
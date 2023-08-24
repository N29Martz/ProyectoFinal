using Dapper;
using ProyectoFinal.Models;
using System.Data.SqlClient;

namespace ProyectoFinal.Servicios
{
    public class RepositorioPedidos : IRepositorioPedidos
    {
        private readonly string connectionString;

        public RepositorioPedidos(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CrearPedido(Pedido modelo)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Pedidos (Nombre, Cantidad, Entrega, Descripcion)
                VALUES (@Nombre, @Cantidad, @Entrega, @Descripcion);
                SELECT SCOPE_IDENTITY()", modelo);
                

            modelo.Id = id;
        }

        public async Task<IEnumerable<Pedido>> ObtenerDatos() //ctrl . agregar pull en la interface
        {
            using var connection = new SqlConnection(connectionString);  //Esto abre la conexion a la base



            return await connection.QueryAsync<Pedido>(
                @"
                    SELECT
	                    Id,
	                    Nombre,
                        Cantidad,
                        Entrega,
	                    Descripcion
                    FROM Pedidos 
                ");//order se usa para que si ordene los campos
        }

        public async Task<Pedido> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Pedido>(
                 @"SELECT
                 Id,
                 Nombre,
                 Cantidad,
                 Entrega,
                 Descripcion
                 FROM Pedidos 
                WHERE Id = @id;
                ", new { id }); //si no encuentra datos devolvera valor por defecto
        }

        public async Task EditarPedido(Pedido modelo)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                    @"UPDATE Pedidos 
                        SET
                        Nombre = @Nombre,
                        Cantidad = @Cantidad,
                        Entrega = @Entrega,
                        Descripcion = @Descripcion
                    WHERE Id = @Id", modelo);
        }

        public async Task EliminarPedido(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                  @"DELETE FROM Pedidos WHERE Id = @id;",
                  new { id });
        }

        public async Task MarcarEntregado(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                @"UPDATE Pedidos 
          SET Entrega = 'Entregado'
          WHERE Id = @id;",
                new { id });
        }

        public async Task AnularEntrega(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                @"UPDATE Pedidos 
          SET Entrega = 'Pendiente'
          WHERE Id = @id;",
                new { id });
        }

        public async Task<IEnumerable<Pedido>> BuscarPorNombre(string busqueda)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Pedido>(
        @"
            SELECT
                Id,
                Nombre,
                Cantidad,
                Entrega,
                Descripcion
            FROM Pedidos
            WHERE Nombre LIKE @busqueda + '%'
        ", new { busqueda });
        }

    }
}

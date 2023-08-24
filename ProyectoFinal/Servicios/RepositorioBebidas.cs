using Dapper;
using ProyectoFinal.Models;
using System.Data.SqlClient;

namespace ProyectoFinal.Servicios
{
    public class RepositorioBebidas : IRepositorioBebidas
    {
        private readonly string connectionString;
        public RepositorioBebidas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CrearBebida(Bebida modelo)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
               @"INSERT INTO Bebidas (Nombre, Marca, Precio)
                VALUES (@Nombre, @Marca, @Precio);
                SELECT SCOPE_IDENTITY()", modelo);
                

            modelo.Id = id;
        }

        public async Task<IEnumerable<Bebida>> ObtenerDatos()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Bebida>(
                @"
                    SELECT
                        Id,
                        Nombre,
                        Marca,
                        Precio
                    FROM Bebidas 
                ");
        }

        public async Task<Bebida> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Bebida>(
               @"SELECT
                 Id,
                 Nombre,
                 Marca,
                 Precio
                 FROM Bebidas 
                WHERE Id = @id;
                ", new { id });
        }

        public async Task EditarBebida(Bebida modelo)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
               @"UPDATE Bebidas SET
                    Nombre = @Nombre,
                    Marca = @Marca,
                    Precio = @Precio
                WHERE Id = @Id", modelo);
        }

        public async Task EliminarBebida(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
               @"DELETE FROM Bebidas WHERE Id = @id", new { id });
        }

    }
}

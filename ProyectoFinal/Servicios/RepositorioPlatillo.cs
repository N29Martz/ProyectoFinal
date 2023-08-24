using Dapper;
using ProyectoFinal.Models;
using System.Data.SqlClient;

namespace ProyectoFinal.Servicios
{
    public class RepositorioPlatillo : IRepositorioPlatillo
    {
        private readonly string connectionString;

        public RepositorioPlatillo(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CrearPlatillo(Platillo modelo)
        {

            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO MComida
                    (Nombre, Img, Descripcion, Precio)
                    VALUES
                    (@Nombre, @Img, @Descripcion, @Precio);

                SELECT SCOPE_IDENTITY();", modelo);

            modelo.Id = id;
        }

        public async Task<IEnumerable<Platillo>> ObtenerDatos()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Platillo>(
                @"SELECT 
	                Id,
	                Nombre,
	                Img,
	                Descripcion,
	                Precio
                FROM MComida
                ");
        }

        public async Task<Platillo> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Platillo>(
                 @"SELECT
	                Id,
	                Nombre,
	                Img,
	                Descripcion,
	                Precio
                FROM MComida
                WHERE Id = @id;
                ", new { id }); //si no encuentra datos devolvera valor por defecto
        }

        public async Task EditarPlatillo(Platillo modelo)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                    @"UPDATE MComida 
                      SET
                      Nombre = @Nombre,
                      Img = @Img,
                      Descripcion = @Descripcion,
                      Precio = @Precio
                    WHERE Id = @Id", modelo);
        }

        public async Task EliminarPlatillo(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                  @"DELETE FROM MComida WHERE Id = @id;",
                  new { id });
        }
    }
}

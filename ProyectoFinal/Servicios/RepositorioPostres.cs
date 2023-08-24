using Dapper;
using ProyectoFinal.Models;
using System.Data.SqlClient;

namespace ProyectoFinal.Servicios
{
    public class RepositorioPostres: IRepositorioPostres
    {
        private readonly string connectionString;

        public RepositorioPostres(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CrearPostre(Postres modelo)
        {

            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Postres
                  (TipoPlatillo,Img, Nombre, Descripcion, Precio)
                  VALUES
                  (@TipoPlatillo, @Img, @Nombre, @Descripcion, @Precio);

                SELECT SCOPE_IDENTITY();", modelo);

            modelo.Id = id;
        }

        public async Task<IEnumerable<Postres>> ObtenerDatos()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Postres>(
                @"SELECT 
	                Id,
                    TipoPlatillo,
	                Img,
	                Nombre,
	                Descripcion,
	                Precio
                FROM Postres
                ");
        }

        public async Task<Postres> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Postres>(
                 @"SELECT
	                Id,
                    TipoPlatillo,
	                Img,
	                Nombre,
	                Descripcion,
	                Precio
                FROM Postres
                WHERE Id = @id;
                ", new { id }); //si no encuentra datos devolvera valor por defecto
        }

        public async Task EditarPostre(Postres modelo)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                    @"UPDATE Postres 
                      SET
                      TipoPlatillo = @TipoPlatillo,
                      Img = @Img,
                      Nombre = @Nombre,
                      Descripcion = @Descripcion,
                      Precio = @Precio
                    WHERE Id = @Id", modelo);
        }

        public async Task EliminaPostre(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                  @"DELETE FROM Postres WHERE Id = @id;",
                  new { id });
        }
    }
}

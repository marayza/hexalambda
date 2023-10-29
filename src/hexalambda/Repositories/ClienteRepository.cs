using Dapper;
using hexalambda.Models;
using Npgsql;

namespace hexalambda.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;
        public ClienteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Cliente?> GetByCPFAsync(string cpf)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT * FROM clientes WHERE cpf = @CPF ";
                var parameters = new { CPF = cpf };

                return await connection.QueryFirstOrDefaultAsync<Cliente>(query, parameters);
            }
        }
    }
}

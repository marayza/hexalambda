using Dapper;
using hexalambda.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using System.Data.SqlClient;

namespace hexalambda.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly string _connectionString;

        public ClienteController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgreSQLConnection");
        }

        [HttpGet]
        public IActionResult AutenticarPorCPF(string cpf)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {

                try
                {
                    connection.Open();
                }
                catch(Exception e)
                {
                    return BadRequest("Erro banco de dados");
                }

                // Use Dapper para executar uma consulta SQL personalizada
                var query = "SELECT * FROM clientes WHERE cpf = @CPF ";
                var parameters = new { CPF = cpf};
                var usuario = connection.QueryFirstOrDefault<Cliente>(query, parameters);

                if (usuario == null)
                    return NotFound("Usuário não encontrado");

                return Ok(usuario);
            }
        }
    }
}

using Amazon;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Dapper;
using hexalambda.Models;
using hexalambda.Repositories;
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
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]

        public async Task<IActionResult> AutenticarPorCPF(string cpf)
        {
            try
            {
                var cliente = await _clienteRepository.GetByCPFAsync(cpf);
                if (cliente != null)
                {
                    return Ok(cliente);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }
        
    }
}

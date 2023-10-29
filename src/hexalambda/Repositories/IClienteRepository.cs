using hexalambda.Models;

namespace hexalambda.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente?> GetByCPFAsync(string cpf);
    }
}

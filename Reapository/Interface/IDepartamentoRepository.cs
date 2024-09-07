using Empresa.DTO;
using Empresa.Models;

namespace Empresa.Reapository.Interface
{
    public interface IDepartamentoRepository
    {
        Task<IEnumerable<Departamento>> GetDepartamentosAsync();
        Task<Departamento> GetDepartamentoAsync(int id);
        Task<Departamento> AddDepartamentoAsync(DepartamentoCreateDTO departamento);
        Task<Departamento> UpdateDepartamentoAsync(Departamento departamento);
        void DeleteDepartamentoAsync(int id);
    }
}


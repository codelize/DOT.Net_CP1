using Empresa.DTO;
using Empresa.Models;

namespace Empresa.Reapository.Interface
{
    public interface IEmpregadoRepository
    {
        Task<IEnumerable<Empregado>> GetEmpregados();
        Task<Empregado> GetEmpregado(int empId);
        Task<List<Empregado>> AddEmpregado(EmpregadosCreateDTO empregadosCreateDTO);
       
        Task<Empregado> UpdateEmpregado(Empregado empregado);
        void DeleteEmpregado(int empId);
        Task<List<Empregado>> GetEmpregadosByDepIdAsync(int depid);
        

    }
}

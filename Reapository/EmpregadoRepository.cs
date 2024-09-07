using Empresa.Data;
using Empresa.DTO;
using Empresa.Models;
using Empresa.Reapository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Reapository
{
    public class EmpregadoRepository : IEmpregadoRepository
    {
        private readonly dbContext dbContext;

        public EmpregadoRepository(dbContext dbContext) { 
            this.dbContext = dbContext;
        }

       

        public async void DeleteEmpregado(int empId)
        {
            var result = await dbContext.Empregados.FirstOrDefaultAsync(e => e.EmpId == empId);
            if (result != null)
            {
                dbContext.Empregados.Remove(result);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<Empregado> GetEmpregado(int empId)
        {
            return await dbContext.Empregados.FirstOrDefaultAsync(e => e.EmpId == empId);
        }

        public async Task<IEnumerable<Empregado>> GetEmpregados()
        {
            return await dbContext.Empregados.ToListAsync();
        }

        public async Task<Empregado> UpdateEmpregado(Empregado empregado)
        {
            var result = await dbContext.Empregados.FirstOrDefaultAsync(e => e.EmpId == empregado.EmpId);
            if (result != null)
            {
                result.Nome = empregado.Nome;
                result.Sobrenome = empregado.Sobrenome;
                result.DepId = empregado.DepId;
                result.Genero = empregado.Genero;
                result.Email = empregado.Email;
                result.FotoUrl = empregado.FotoUrl;

                await dbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }




        public async Task<List<Empregado>> GetEmpregadosByDepIdAsync(int depid)
        {
            try
            {
                // Busca os empregados com base no DepId
                var empregados = await dbContext.Empregados
                                                .Where(e => e.DepId == depid)
                                                .ToListAsync();

                if (empregados == null || !empregados.Any())
                {
                    throw new Exception("Nenhum registro de empregados localizado!");
                }

                // Retorna a lista de empregados
                return empregados;
            }
            catch (Exception ex)
            {
                // Lança uma exceção em caso de erro
                throw new Exception($"Erro ao buscar empregados por departamento: {ex.Message}");
            }
        }

        public async Task<List<Empregado>> AddEmpregado(EmpregadosCreateDTO empregadosCreateDTO)
        {
            try
            {
                // Verificar se o departamento existe
                var departamento = await dbContext.departamentos
                    .FirstOrDefaultAsync(dep => dep.DepId == empregadosCreateDTO.DepId);

                if (departamento == null)
                {
                    throw new Exception("Nenhum registro de departamento localizado!");
                }

                // Criar o novo empregado
                var empregado = new Empregado()
                {
                    Nome = empregadosCreateDTO.Nome,
                    Sobrenome = empregadosCreateDTO.Sobrenome,
                    Email = empregadosCreateDTO.Email,
                    Genero = empregadosCreateDTO.Genero,
                    DepId = empregadosCreateDTO.DepId,
                    Departamento = departamento
                };

                // Adicionar o empregado no contexto
                dbContext.Add(empregado);
                await dbContext.SaveChangesAsync();

                // Retornar a lista de empregados com os departamentos incluídos
                return await dbContext.Empregados.Include(e => e.Departamento).ToListAsync();
            }
            catch (Exception ex)
            {
                // Lançar uma exceção ou lidar com o erro conforme necessário
                throw new Exception($"Erro ao criar empregado: {ex.Message}");
            }
        }
    }
}

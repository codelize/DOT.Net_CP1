using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Empresa.Data;
using Empresa.Models;
using Empresa.Reapository.Interface;
using Empresa.DTO;

namespace Empresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoRepository _departamentoRepository;

        public DepartamentoController(IDepartamentoRepository _departamentoRepository)
        {
            this._departamentoRepository = _departamentoRepository;
        }
        // GET: api/Departamento
        [HttpGet]
        public async Task<IActionResult> GetDepartamentos()
        {
            try
            {
                var departamentos = await _departamentoRepository.GetDepartamentosAsync();
                return Ok(departamentos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar dados do banco de dados");
            }
        }

        // GET: api/Departamento/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDepartamento(int id)
        {
            try
            {
                var departamento = await _departamentoRepository.GetDepartamentoAsync(id);

                if (departamento == null)
                    return NotFound($"Departamento com id = {id} não encontrado");

                return Ok(departamento);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar dados do banco de dados");
            }
        }

        // PUT: api/Departamento/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDepartamento([FromBody] Departamento departamento)
        {
            try
            {
                var existingDepartamento = await _departamentoRepository.GetDepartamentoAsync(departamento.DepId);

                if (existingDepartamento == null)
                    return NotFound($"Departamento com id = {departamento.DepId} não encontrado");

                var updatedDepartamento = await _departamentoRepository.UpdateDepartamentoAsync(departamento);
                return Ok(updatedDepartamento);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no banco de dados");
            }
        }

        // POST: api/Departamento
        [HttpPost("CreateDepartamento")]
        public async Task<ActionResult<Departamento>> CreateDepartamento([FromBody] DepartamentoCreateDTO departamentoCreateDto)
        {
            try
            {
                if (departamentoCreateDto == null)
                    return BadRequest("Dados do departamento são inválidos.");

                var createdDepartamento = await _departamentoRepository.AddDepartamentoAsync(departamentoCreateDto);

                return Ok(createdDepartamento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar o departamento: {ex.Message}");
            }
        }

        // DELETE: api/Departamento/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDepartamento(int id)
        {
            try
            {
                var departamento = await _departamentoRepository.GetDepartamentoAsync(id);

                if (departamento == null)
                    return NotFound($"Departamento com id = {id} não encontrado");

                _departamentoRepository.DeleteDepartamentoAsync(id);
                return Ok(departamento);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados do banco de dados");
            }
        }
    }
    }

using Empresa.Models;

namespace Empresa.DTO
{
    public class EmpregadosCreateDTO
    {
        public string Nome { get; set; } 
        public string Sobrenome { get; set; } 
        public string Email { get; set; } 
        public Genero Genero { get; set; }
        public string fotoUrl { get; set; } 
        public VinculoDTO Vinculo { get; set; }
        public int DepId { get; set; }
    }
}

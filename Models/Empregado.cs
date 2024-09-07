using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empresa.Models
{
    [Table("Empregados_PX")]
    public class Empregado
    {
        [Key]
        public int EmpId { get; set; }
        public string Nome { get; set; } 
        public string Sobrenome { get; set; } 
        public string Email { get; set; } 
        public Genero Genero { get; set; }
        public string? FotoUrl { get; set; }
        
        [ForeignKey("DepId")]
        public Departamento? Departamento { get; set; }
        public int DepId { get; set; }
    }
}

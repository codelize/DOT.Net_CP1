using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Empresa.Models
{
    [Table("Departamentos_PX")]
    public class Departamento
    {
        [Key]
        public int DepId { get; set; }
        public string DepNome { get; set; }
        [JsonIgnore]
        public ICollection<Empregado> Empregados { get; set; } = new List<Empregado>();

    }
}

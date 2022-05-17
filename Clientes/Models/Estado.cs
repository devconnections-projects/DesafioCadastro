using Clientes.Data;
using System.ComponentModel.DataAnnotations;

namespace Clientes.Models
{
    public class Estado
    {
        [Key]
        public int EstadoId { get; set; }

        public string NomeEstado { get; set; }

        public string Sigla { get; set; }

        
    }
}
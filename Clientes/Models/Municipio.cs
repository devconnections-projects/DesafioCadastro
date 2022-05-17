using Clientes.Data;
using System.ComponentModel.DataAnnotations;

namespace Clientes.Models
{
    public class Municipio
    {
        public int MunicipioId { get; set; }

        public string NomeMunicipio { get; set; }

        public int EstadId { get; set; }

        public virtual Estado Estado { get; set; }

    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clientes.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string CNPJ { get; set; }

        public string RazaoSocial { get; set; }

        public string Porte { get; set; }

        public int EstadoId { get; set; }

        public int MunicipioId { get; set; }

    }
}
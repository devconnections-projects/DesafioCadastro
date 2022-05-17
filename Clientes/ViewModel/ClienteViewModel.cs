using Clientes.Models;

namespace Clientes.ViewModel
{
    public class ClienteViewModel
    {
        public int ClienteId { get; set; }

        public string CNPJ { get; set; }

        public string RazaoSocial { get; set; }

        public string Porte { get; set; }

        public string Estado { get; set; }

        public string Municipio { get; set; }

    }
}

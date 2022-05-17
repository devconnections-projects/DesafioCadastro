using DesafioCadastro.Data;
using System.ComponentModel.DataAnnotations;

namespace DesafioCadastro.Models
{
    public class Municipio
    {
        /*
        public Municipio(int estado, string nomeMunicipio)
        {
            this.NomeMunicipio = nomeMunicipio;
            this.EstadId = estado;
        }*/

        [Key]
        public int MunicipioId { get; set; }

        [Required(ErrorMessage = "O nome do miunicípio é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do município não deve ultrapassar {1} caracteres")]
        public string NomeMunicipio { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório")]
        public int EstadId { get; set; }

        public virtual Estado Estado { get; set; }

    }
}

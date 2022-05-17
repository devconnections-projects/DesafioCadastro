using DesafioCadastro.Data;
using System.ComponentModel.DataAnnotations;

namespace DesafioCadastro.Models
{
    public class Estado
    {
        /*
        public Estado(string sigla, string nomeEstado)
        {
            this.NomeEstado = nomeEstado;
            this.Sigla = sigla;
        }*/

        [Key]
        public int EstadoId { get; set; }

        [Required(ErrorMessage = "O nome do estado é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome do estado não pode conter mais que {1} caracteres.")]
        public string NomeEstado { get; set; }

        [Required(ErrorMessage = "A sigla do estado é obrigatória")]
        [StringLength(2, ErrorMessage = "A sigla do estado não pode conter mais que {1} caracteres.")]
        public string Sigla { get; set; }

        
    }
}
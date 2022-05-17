using DesafioCadastro.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioCadastro.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O CNPJ do cliente é obrigatório")]
        [StringLength(18, ErrorMessage = "O CNPJ do cliente não pode ter mais que {1} caractere(s)")]
        [RegularExpression(@"[0-9]{2}\.?[0-9]{3}\.?[0-9]{3}\/?[0-9]{4}\-?[0-9]{2}")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "A razão social do cliente deve ser informada")]
        [StringLength(70, ErrorMessage = "A razão social do cliente não pode conter mais que {1} caractere")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "O porte do cliente deve ser selecionado")]
        [StringLength(7, ErrorMessage = "O porte do cliente não pode conter mais que {1} caractere")]
        public string Porte { get; set; }

        [Display(Name = "Estado")]
        [ForeignKey("EstadoId")]
        [Required(ErrorMessage = "Um estado deve ser selecionado")]
        public int EstadoId { get; set; }

        [Required(ErrorMessage = "Um município deve ser selecionado")]
        [Display(Name = "Município")]
        [ForeignKey("MunicipioId")]
        public int MunicipioId { get; set; }

    }
}
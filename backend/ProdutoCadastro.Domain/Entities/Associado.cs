using System.ComponentModel.DataAnnotations;

namespace ProdutoCadastro.Domain.Entities
{
    public class Associado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Nome { get; set; }

        [Required, StringLength(11)]
        public required long CPF { get; set; }

        [Required, StringLength(7)]
        public required string Placa { get; set; }

        [Required]
        public required string Endereco { get; set; }

        [Required, StringLength(11)]
        public required long Telefone { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ProdutoCadastro.Domain.Entities
{
    public class Associado
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(40)]
        public required string Nome { get; set; }

        [Required, StringLength(11)]
        public required long CPF { get; set; }

        [Required, StringLength(7)]
        public required string Placa { get; set; }

        [Required]
        public required string Endereco { get; set; }

        [Required, StringLength(11)]
        public required long Telefone { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Nome.ToString().Length > 40)
            {
                yield return new ValidationResult("O Nome deve possuir no máximo 40 caracteres", new[] { nameof(CPF) });
            }

            if (CPF.ToString().Length != 11)
            {
                yield return new ValidationResult("O CPF deve conter exatamente 11 dígitos.", new[] { nameof(CPF) });
            }

            if (Telefone.ToString().Length != 11)
            {
                yield return new ValidationResult("O telefone deve conter exatamente 11 dígitos.", new[] { nameof(Telefone) });
            }

            if (Placa.Length != 7)
            {
                yield return new ValidationResult("A placa deve conter exatamente 7 caracteres.", new[] { nameof(Placa) });
            }
        }
    }
}

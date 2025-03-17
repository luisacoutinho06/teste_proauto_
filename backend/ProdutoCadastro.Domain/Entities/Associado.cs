using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProdutoCadastro.Domain.Entities
{
    public class Associado : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(40)]
        public required string Nome { get; set; }

        [Required]
        public required long CPF { get; set; }

        [Required, StringLength(7)]
        public required string Placa { get; set; }

        [Required]
        public required string Endereco { get; set; }

        [Required]
        public required long Telefone { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nome) && Nome.Length > 40)
            {
                yield return new ValidationResult("O Nome deve possuir no máximo 40 caracteres", new[] { nameof(Nome) });
            }

            string cpfString = CPF.ToString();
            if (cpfString.Length < 10 && cpfString.Length > 11)
            {
                yield return new ValidationResult("O CPF deve conter exatamente 11 dígitos.", new[] { nameof(CPF) });
            }

            string telefoneString = Telefone.ToString();
            if (telefoneString.Length != 11)
            {
                yield return new ValidationResult("O telefone deve conter exatamente 11 dígitos.", new[] { nameof(Telefone) });
            }

            if (string.IsNullOrEmpty(Placa) || Placa.Length != 7)
            {
                yield return new ValidationResult("A placa deve conter exatamente 7 caracteres.", new[] { nameof(Placa) });
            }
        }
    }
}

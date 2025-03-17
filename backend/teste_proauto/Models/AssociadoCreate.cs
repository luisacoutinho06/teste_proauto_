namespace ProdutoCadastro.API.Models
{
    public class AssociadoCreate
    {
        public int Id { get; set; } 
        public required string Nome { get; set; }
        public required string CPF { get; set; } 
        public required string Placa { get; set; } 
        public required string Endereco { get; set; }
        public required string Telefone { get; set; }
    }
}

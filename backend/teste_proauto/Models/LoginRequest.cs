namespace ProautoCadastro.API.Models
{
    public class LoginRequest
    {
        public required string CPF { get; set; }
        public required string Placa { get; set; }
    }
}

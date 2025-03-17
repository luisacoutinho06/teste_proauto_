using ProdutoCadastro.Domain.Entities;

namespace ProdutoCadastro.Services.Interface
{
    public interface IAssociadoService
    {
        Task<Associado?> ObterDadosAsync(string cpf, string placa);
        Task AtualizarEnderecoAsync(int id, string novoEndereco);
        Task<Associado?> ObterPeloIdAssociadoAsync(int id);
        Task CriarAssociadoAsync(Associado novoAssociado);
        Task DeletarAssociadoAsync(int id);
    }
}

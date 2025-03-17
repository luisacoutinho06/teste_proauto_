using ProdutoCadastro.Domain.Entities;

namespace ProautoCadastro.Data
{
    public interface IAssociadoRepository
    {
        Task<Associado?> ObterPorCpfEPlacaAsync(string cpf, string placa);
        Task<Associado?> ObterPeloIdAssociadoAsync(int id);
        Task AtualizarEnderecoAsync(Associado associado);
        Task CriarAssociadoAsync(Associado novoAssociado);
        Task DeletarAssociadoAsync(int id);
    }
}

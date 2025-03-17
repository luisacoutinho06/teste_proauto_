using ProautoCadastro.Data;
using ProdutoCadastro.Domain.Entities;
using ProdutoCadastro.Services.Interface;

namespace ProdutoCadastro.Services.Services
{
    public class AssociadoService(IAssociadoRepository associadoRepository) : IAssociadoService
    {
        private readonly IAssociadoRepository _associadoRepository = associadoRepository;

        public async Task<Associado?> ObterDadosAsync(string cpf, string placa)
        {
            return await _associadoRepository.ObterPorCpfEPlacaAsync(cpf, placa);
        }

        public async Task AtualizarEnderecoAsync(int id, string novoEndereco)
        {
            var associado = await _associadoRepository.ObterPeloIdAssociadoAsync(id);
            if (associado != null)
            {
                associado.Endereco = novoEndereco;
                await _associadoRepository.AtualizarEnderecoAsync(associado);
            }
        }

        public async Task<Associado?> ObterPeloIdAssociadoAsync(int id)
        {
            return await _associadoRepository.ObterPeloIdAssociadoAsync(id);
        }

        public async Task CriarAssociadoAsync(Associado novoAssociado)
        {
             await _associadoRepository.CriarAssociadoAsync(novoAssociado);
        }

        public async Task DeletarAssociadoAsync(int id)
        {
             await _associadoRepository.DeletarAssociadoAsync(id);
        }
    }
}

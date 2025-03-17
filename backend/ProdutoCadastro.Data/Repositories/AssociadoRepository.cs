using Microsoft.EntityFrameworkCore;
using ProautoCadastro.Data;
using ProdutoCadastro.Data.Context;
using ProdutoCadastro.Domain.Entities;

namespace ProdutoCadastro.Data.Repositories
{
    public class AssociadoRepository(ApplicationDbContext context) : IAssociadoRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Associado?> ObterPorCpfEPlacaAsync(string cpf, string placa)
        {
            cpf = RemoverMascara(cpf);
            return await _context.Associados.FirstOrDefaultAsync(a => a.CPF == cpf && a.Placa == placa);
        }

        public async Task AtualizarEnderecoAsync(Associado associado)
        {
            _context.Associados.Update(associado);
            await _context.SaveChangesAsync();
        }

        public async Task<Associado?> ObterPeloIdAssociadoAsync(int id)
        {
            return await _context.Associados.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task CriarAssociadoAsync(Associado novoAssociado)
        {
            novoAssociado.CPF = RemoverMascara(novoAssociado.CPF);
            novoAssociado.Telefone = RemoverMascara(novoAssociado.Telefone);

            _context.Associados.Add(novoAssociado);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAssociadoAsync(int id)
        {
            var associado = await _context.Associados.FindAsync(id);
            if (associado != null)
            {
                _context.Associados.Remove(associado);
                await _context.SaveChangesAsync();
            }
        }


        private string RemoverMascara(string valor)
        {
            return string.IsNullOrEmpty(valor) ? valor : new string(valor.Where(char.IsDigit).ToArray());
        }
    }
}

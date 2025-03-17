using Microsoft.EntityFrameworkCore;
using ProautoCadastro.Data;
using ProdutoCadastro.Data.Context;
using ProdutoCadastro.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProdutoCadastro.Data.Repositories
{
    public class AssociadoRepository(ApplicationDbContext context) : IAssociadoRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Associado?> ObterPorCpfEPlacaAsync(string cpf, string placa)
        {
            long cpfFormatado = Int64.Parse(cpf);
            return await _context.Associados.FirstOrDefaultAsync(a => a.CPF == cpfFormatado && a.Placa == placa);
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
            var context = new ValidationContext(novoAssociado);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(novoAssociado, context, results, true);

            if (!isValid)
            {
                var erros = string.Join(", ", results.Select(r => r.ErrorMessage));
                throw new Exception($"Erro de validação: {erros}");
            }

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

        public async Task<Associado?> ObterDadosEValidarCPFePlacaAsync(string cpf, string placa)
        {
            var associadoPorCpf = await _context.Associados
                .FirstOrDefaultAsync(a => a.CPF == long.Parse(cpf));

            if (associadoPorCpf != null)
                return associadoPorCpf; 


            var associadoPorPlaca = await _context.Associados
                .FirstOrDefaultAsync(a => a.Placa == placa);

            if (associadoPorPlaca != null)
                return associadoPorPlaca;

            return null;
        }
    }
}

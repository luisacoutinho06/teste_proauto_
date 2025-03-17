using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProautoCadastro.API.Models;
using ProdutoCadastro.API.Models;
using ProdutoCadastro.Domain.Entities;
using ProdutoCadastro.Services.Interface;

namespace ProdutoCadastro.API.Controllers
{
    [ApiController]
    [Route("api/associado")]
    public class AssociadoController(IAssociadoService associadoService) : ControllerBase
    {
        private readonly IAssociadoService _associadoService = associadoService;

        [HttpPost("autenticar")]
        public async Task<IActionResult> Autenticar([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.CPF) || string.IsNullOrEmpty(request.Placa))
                return BadRequest("CPF e Placa são obrigatórios.");

            var associado = await _associadoService.ObterDadosAsync(request.CPF, request.Placa);

            if (associado == null)
                return Unauthorized(new { message = "CPF ou Placa inválidos." });

            return Ok(associado);
        }

        [HttpPut("atualizar-endereco/{id}")]
        public async Task<IActionResult> AtualizarEndereco(int id, [FromBody] string novoEndereco)
        {
            if (string.IsNullOrEmpty(novoEndereco))
                return BadRequest("Novo endereço não pode ser vazio.");

            await _associadoService.AtualizarEnderecoAsync(id, novoEndereco);

            return Ok(new { message = "Endereço atualizado com sucesso." });
        }

        [HttpPost("criar")]
        public async Task<IActionResult> Criar([FromBody] AssociadoCreate novoAssociado)
        {
            if (novoAssociado == null)
                return BadRequest("Dados do associado são obrigatórios.");

            if (string.IsNullOrEmpty(novoAssociado.CPF) || string.IsNullOrEmpty(novoAssociado.Nome) ||
                string.IsNullOrEmpty(novoAssociado.Placa) || string.IsNullOrEmpty(novoAssociado.Endereco) 
                || string.IsNullOrEmpty(novoAssociado.Telefone))
                return BadRequest("Dados do associado são obrigatórios.");

            var associadoExistente = await _associadoService.ObterDadosAsync(novoAssociado.CPF, novoAssociado.Placa);
            if (associadoExistente != null)
                return Conflict(new { message = "Associado já existe." });


            var associadoEntity = new Associado
            {
                Nome = novoAssociado.Nome,
                CPF = novoAssociado.CPF,
                Placa = novoAssociado.Placa,
                Endereco = novoAssociado.Endereco,
                Telefone = novoAssociado.Telefone
            };

            await _associadoService.CriarAssociadoAsync(associadoEntity);

            return CreatedAtAction(nameof(ObterPorId), new { id = novoAssociado.Id }, novoAssociado);
        }

        [HttpDelete("deletar/{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var associado = await _associadoService.ObterPeloIdAssociadoAsync(id);
            if (associado == null)
                return NotFound(new { message = "Associado não encontrado." });

            await _associadoService.DeletarAssociadoAsync(id);

            return Ok(new { message = "Associado excluído com sucesso." });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var associado = await _associadoService.ObterPeloIdAssociadoAsync(id);
            if (associado == null)
                return NotFound(new { message = "Associado não encontrado." });

            return Ok(associado);
        }
    }
}

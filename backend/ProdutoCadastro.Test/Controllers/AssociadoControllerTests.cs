using Microsoft.AspNetCore.Mvc;
using Moq;
using ProautoCadastro.API.Models;
using ProdutoCadastro.API.Controllers;
using ProdutoCadastro.API.Models;
using ProdutoCadastro.Services.Interface;

public class AssociadoControllerTests
{
    private readonly AssociadoController _controller;
    private readonly Mock<IAssociadoService> _associadoServiceMock;

    public AssociadoControllerTests()
    {
        _associadoServiceMock = AssociadoServiceMock.Criar();
        _controller = new AssociadoController(_associadoServiceMock.Object);
    }

    // 🔹 Teste: Autenticação bem-sucedida
    [Fact]
    public async Task Autenticar_DeveRetornarOk_QuandoAssociadoExiste()
    {
        var request = new LoginRequest { CPF = "12345678900", Placa = "ABC1234" };

        var result = await _controller.Autenticar(request);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    // 🔹 Teste: Autenticação falha com credenciais inválidas
    [Fact]
    public async Task Autenticar_DeveRetornarUnauthorized_QuandoAssociadoNaoExiste()
    {
        var request = new LoginRequest { CPF = "00000000000", Placa = "XYZ9999" };

        var result = await _controller.Autenticar(request);

        Assert.IsType<UnauthorizedObjectResult>(result);
    }

    // 🔹 Teste: Atualização de endereço bem-sucedida
    [Fact]
    public async Task AtualizarEndereco_DeveRetornarOk_QuandoEnderecoForAtualizado()
    {
        var result = await _controller.AtualizarEndereco(1, "Novo Endereço 123");

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Endereço atualizado com sucesso.", ((dynamic)okResult.Value).message);
    }

    // 🔹 Teste: Atualização falha se o endereço estiver vazio
    [Fact]
    public async Task AtualizarEndereco_DeveRetornarBadRequest_SeEnderecoForVazio()
    {
        var result = await _controller.AtualizarEndereco(1, "");

        Assert.IsType<BadRequestObjectResult>(result);
    }

    // 🔹 Teste: Criar associado com sucesso
    [Fact]
    public async Task Criar_DeveRetornarCreated_QuandoAssociadoForCriado()
    {
        var novoAssociado = new AssociadoCreate
        {
            CPF = "99999999999",
            Nome = "Novo Associado",
            Placa = "XYZ1234",
            Endereco = "Rua Nova, 456",
            Telefone = "31987654321"
        };

        var result = await _controller.Criar(novoAssociado);

        Assert.IsType<CreatedAtActionResult>(result);
    }

    // 🔹 Teste: Criar associado falha se já existir
    [Fact]
    public async Task Criar_DeveRetornarConflict_SeAssociadoJaExistir()
    {
        var novoAssociado = new AssociadoCreate
        {
            CPF = "12345678900",
            Nome = "Luisa Coutinho",
            Placa = "ABC1234",
            Endereco = "Rua Exemplo, 123",
            Telefone = "31987654321"
        };

        var result = await _controller.Criar(novoAssociado);

        Assert.IsType<ConflictObjectResult>(result);
    }

    // 🔹 Teste: Criar associado falha se os dados estiverem incompletos
    [Fact]
    public async Task Criar_DeveRetornarBadRequest_SeDadosForemInvalidos()
    {
        var novoAssociado = new AssociadoCreate
        {
            CPF = "12345678901110",
            Nome = "Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste ",
            Endereco = "Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste Teste ",
            Placa = "Teste Teste Teste ",
            Telefone = "31981288282222"
        }; // Dados a mais

        var result = await _controller.Criar(novoAssociado);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    // 🔹 Teste: Deletar associado com sucesso
    [Fact]
    public async Task Deletar_DeveRetornarOk_QuandoAssociadoForExcluido()
    {
        var result = await _controller.Deletar(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Associado excluído com sucesso.", ((dynamic)okResult.Value).message);
    }

    // 🔹 Teste: Deletar falha se o associado não existir
    [Fact]
    public async Task Deletar_DeveRetornarNotFound_SeAssociadoNaoExistir()
    {
        var result = await _controller.Deletar(999); // ID não existente

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // 🔹 Teste: Obter associado por ID com sucesso
    [Fact]
    public async Task ObterPorId_DeveRetornarOk_QuandoAssociadoForEncontrado()
    {
        var result = await _controller.ObterPorId(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    // 🔹 Teste: Obter associado falha se ID não existir
    [Fact]
    public async Task ObterPorId_DeveRetornarNotFound_SeAssociadoNaoForEncontrado()
    {
        var result = await _controller.ObterPorId(999); // ID não existente

        Assert.IsType<NotFoundObjectResult>(result);
    }
}

using Moq;
using ProdutoCadastro.Domain.Entities;
using ProdutoCadastro.Services.Interface;

public static class AssociadoServiceMock
{
    public static Mock<IAssociadoService> Criar()
    {
        var mock = new Mock<IAssociadoService>();

        // Simular um associado válido
        var associadoFake = new Associado
        {
            Id = 1,
            Nome = "Luisa Coutinho",
            CPF = 12345678900,
            Placa = "ABC1234",
            Endereco = "Rua Exemplo, 123",
            Telefone = 31987654321
        };

        // Simular método de obtenção de associado por CPF e Placa
        mock.Setup(service => service.ObterPorCpfEPlacaAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((string cpf, string placa) =>
                cpf == "12345678900" && placa == "ABC1234" ? associadoFake : null);

        // Simular método de obtenção por ID
        mock.Setup(service => service.ObterPeloIdAssociadoAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => id == 1 ? associadoFake : null);

        // Simular método de criação
        mock.Setup(service => service.CriarAssociadoAsync(It.IsAny<Associado>()))
            .Returns(Task.CompletedTask);

        // Simular método de atualização de endereço
        mock.Setup(service => service.AtualizarEnderecoAsync(It.IsAny<int>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        // Simular método de deletar associado
        mock.Setup(service => service.DeletarAssociadoAsync(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        // Simular método de validação de CPF e Placa para verificar duplicação
        mock.Setup(service => service.ObterDadosEValidarCPFePlacaAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((string cpf, string placa) =>
                cpf == "12345678900" && placa == "ABC1234" ? associadoFake : null);

        return mock;
    }
}

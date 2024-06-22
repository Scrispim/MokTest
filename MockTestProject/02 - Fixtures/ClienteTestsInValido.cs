using System;
using Features.Clientes;
using Xunit;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClienteTestsFixture))]
    public class ClienteTestsInValido : IClassFixture<ClienteTestsFixture>
    {        
        readonly ClienteTestsFixture _fixtureCliente;

        public ClienteTestsInValido(ClienteTestsFixture fixtureCliente)
        {
            _fixtureCliente = fixtureCliente;
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Trait Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _fixtureCliente.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            Assert.False(result);
            Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);
        }
    }
}
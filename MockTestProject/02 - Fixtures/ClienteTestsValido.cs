using System;
using Features.Clientes;
using Xunit;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClienteTestsFixture))]
    public class ClienteTestsValido : IClassFixture<ClienteTestsFixture>
    {
        readonly ClienteTestsFixture _fixtureCliente;

        public ClienteTestsValido(ClienteTestsFixture fixtureCliente)
        {
            _fixtureCliente = fixtureCliente;
        }

        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria","Cliente Trait Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = _fixtureCliente.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            Assert.False(result);
            Assert.Equal(0, cliente.ValidationResult.Errors.Count);
        }
    }
}
using System.Linq;
using System.Threading;
using Features.Clientes;
using MediatR;
using Moq;
using NuGet.Frameworks;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServiceTests
    {
        readonly ClienteTestsBogusFixture _clienteTestsBogus;

        public ClienteServiceTests(ClienteTestsBogusFixture clienteTestsFixture)
        {
            _clienteTestsBogus = clienteTestsFixture;
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            //Arrange
            var cliente = _clienteTestsBogus.GerarClienteValido();
            //here create the repository mock object
            var clienteRepo = new Mock<IClienteRepository>();
            var mediatr = new Mock<IMediator>();

            var clienteService = new ClienteService(clienteRepo.Object, mediatr.Object);

            //Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            // now to verify if add in repositoy method and called notificaton method
            clienteRepo.Verify(r => r.Adicionar(cliente), Times.Once);
            mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);

        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            //Arrange
            var cliente = _clienteTestsBogus.GerarClienteInvalido();
            //here create the repository mock object
            var clienteRepo = new Mock<IClienteRepository>();
            var mediatr = new Mock<IMediator>();

            var clienteService = new ClienteService(clienteRepo.Object, mediatr.Object);

            //Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.False(cliente.EhValido());
            // now to verify if add in repositoy method and called notificaton method
            clienteRepo.Verify(r => r.Adicionar(cliente), Times.Never);
            mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            //Arrange
            //here create the repository mock object
            var clienteRepo = new Mock<IClienteRepository>();
            var mediatr = new Mock<IMediator>();
            
            //Now is need learn the method what sort for it return, in this case a list of clientes active.
            clienteRepo.Setup(r => r.ObterTodos()).Returns(_clienteTestsBogus.GerarClientesVariados());

            var clienteService = new ClienteService(clienteRepo.Object, mediatr.Object);

            //Act
            var cliente = clienteService.ObterTodosAtivos();

            // Assert
            // now to verify if add in repositoy method and called notificaton method
            clienteRepo.Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(cliente.Any());
            Assert.False(cliente.Count(c => !c.Ativo) > 0);

        }
    }
}
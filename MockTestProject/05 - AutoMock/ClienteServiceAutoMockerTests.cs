using System.Linq;
using System.Threading;
using Features.Clientes;
using MediatR;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServiceAutoMockerTests
    {
        readonly ClienteTestsBogusFixture _clienteTestsBogus;

        public ClienteServiceAutoMockerTests(ClienteTestsBogusFixture clienteTestsFixture)
        {
            _clienteTestsBogus = clienteTestsFixture;
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            //Arrange
            var cliente = _clienteTestsBogus.GerarClienteValido();

            var mock = new AutoMocker();
            // Automocker get all properties needed as mediatr and cliente repositry
            var clienteService = mock.CreateInstance<ClienteService>();

            //Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            // now to verify if add in repositoy method and called notificaton method
            mock.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);
            mock.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);

        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            //Arrange
            var cliente = _clienteTestsBogus.GerarClienteInvalido();

            var mocker = new AutoMocker();

            var clienteService = mocker.CreateInstance<ClienteService>();

            //Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.False(cliente.EhValido());
            // now to verify if add in repositoy method and called notificaton method
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            //Arrange
            //here create the repository mock object
            var clienteRepo = new Mock<IClienteRepository>();
            var mediatr = new Mock<IMediator>();

            var mocker = new AutoMocker();

            //Now is need learn the method what sort for it return, in this case a list of clientes active.
            mocker.GetMock<IClienteRepository>().Setup(r => r.ObterTodos()).Returns(_clienteTestsBogus.GerarClientesVariados());

            var clienteService = mocker.CreateInstance<ClienteService>();

            //Act
            var cliente = clienteService.ObterTodosAtivos();

            // Assert
            // now to verify if add in repositoy method and called notificaton method
            mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(cliente.Any());
            Assert.False(cliente.Count(c => !c.Ativo) > 0);

        }
    }
}
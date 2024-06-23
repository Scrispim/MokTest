using Features.Clientes;
using Features.Produtos;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Tests
{
    [Collection(nameof(ProdutoAutoMockerCollection))]
    public class ProdutoServiceAutoMockerFixtureTests
    {
        readonly ProdutoTestsAutoMockerFixture _fixture;
        private readonly ProdutoService _service;

        public ProdutoServiceAutoMockerFixtureTests(ProdutoTestsAutoMockerFixture fixture)
        {
            _fixture = fixture;
            _service = _fixture.ObterProdutoService();
        }

        [Fact(DisplayName = "Adicionar Produto com Sucesso")]
        [Trait("Categoria", "Produto Service AutoMockFixture Tests")]
        public void ProdutoService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var produto = _fixture.GerarProdutoValido();

            // Act
            _service.Adicionar(produto);

            // Assert
            Assert.True(produto.EhValido());
            _fixture.Mocker.GetMock<IProdutoRepository>().Verify(r => r.Adicionar(produto), Times.Once);
            _fixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }
    }
}

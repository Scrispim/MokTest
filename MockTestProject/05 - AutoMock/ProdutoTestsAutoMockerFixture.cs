using Bogus.DataSets;
using Bogus;
using Features.Clientes;
using Features.Produtos;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ProdutoAutoMockerCollection))]
    public class ProdutoAutoMockerCollection : ICollectionFixture<ProdutoTestsAutoMockerFixture>
    {
    }
    public class ProdutoTestsAutoMockerFixture : IDisposable
    {
        public ProdutoService ProdutoService;
        public AutoMocker Mocker;
        public Produto GerarProdutoValido()
        {
            return GerarProdutos(1, true).FirstOrDefault();
        }

        public IEnumerable<Produto> GerarProdutos(int quantidade, bool ativo)
        {
            //var email = new Faker().Internet.Email("eduardo","pires","gmail");
            //var clientefaker = new Faker<Cliente>();
            //clientefaker.RuleFor(c => c.Nome, (f, c) => f.Name.FirstName());

            var produtos = new Faker<Produto>("pt_BR")
                .CustomInstantiator(f => new Produto(
                    Guid.NewGuid(),
                    f.Vehicle.Model(),
                    f.Vehicle.Manufacturer(),
                    f.Date.Past(80, DateTime.Now.AddYears(-18)),
                    f.Internet.Email(),
                    ativo,
                    DateTime.Now));

            return produtos.Generate(quantidade);
        }

        public Produto GerarProdutoInvalido()
        {
            var produto = new Faker<Produto>("pt_BR")
                .CustomInstantiator(f => new Produto(
                    Guid.NewGuid(),
                    f.Vehicle.Model(),
                    f.Vehicle.Manufacturer(),
                    f.Date.Past(1, DateTime.Now.AddYears(1)),
                    "",
                    false,
                    DateTime.Now));

            return produto;
        }

        public ProdutoService ObterProdutoService()
        {
            Mocker = new AutoMocker();
            ProdutoService = Mocker.CreateInstance<ProdutoService>();

            return ProdutoService;
        }

        public void Dispose()
        {
        }
    }
}

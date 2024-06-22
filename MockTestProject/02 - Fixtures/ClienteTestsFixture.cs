using Bogus;
using Bogus.DataSets;
using Features.Clientes;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClienteTestsFixture))]
    public class ClienteCollection : ICollectionFixture<ClienteTestsFixture>
    {
    }
    //How the class gonna be reused by others class, need implement IDisposable
    public class ClienteTestsFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var cliente = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f => new Cliente(
                    Guid.NewGuid(),
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    f.Date.Past(80, DateTime.Now.AddYears(-18)),
                    "",
                    true,
                    DateTime.Now))
                .RuleFor(c => c.Email, (f, c) =>
                f.Internet.Email(c.Nome.ToLower(), c.Sobrenome.ToLower()));
             

                return cliente;
        }

        public Cliente GerarClienteInvalido()
        {
            // Arrange
            var cliente = new Cliente(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                "edu2edu.com",
                true,
                DateTime.Now);

                return cliente;
        }
        public void Dispose()
        {
        }
    }
}

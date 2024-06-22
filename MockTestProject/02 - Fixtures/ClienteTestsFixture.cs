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
            var cliente = new Cliente(
                Guid.NewGuid(),
                "Eduardo",
                "Pires",
                DateTime.Now.AddYears(-30),
                "edu@edu.com",
                true,
                DateTime.Now);

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

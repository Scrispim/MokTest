using Bogus.DataSets;
using Bogus;
using Features.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClienteBogusCollection))]
    public class ClienteBogusCollection : ICollectionFixture<ClienteTestsBogusFixture>
    {
    }
    //How the class gonna be reused by others class, need implement IDisposable
    public class ClienteTestsBogusFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        { 
            return GerarClientes(1, true).FirstOrDefault();
        }
        public IEnumerable<Cliente> GerarClientesVariados()
        {
            var list = new List<Cliente>();
            list.AddRange(GerarClientes(50, true));
            list.AddRange(GerarClientes(50, false));

            return list;
        }
        public IEnumerable<Cliente> GerarClientes(int quantity, bool active)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var cliente = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f => new Cliente(
                    Guid.NewGuid(),
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    f.Date.Past(80, DateTime.Now.AddYears(-18)),
                    "",
                    active,
                    DateTime.Now))
                .RuleFor(c => c.Email, (f, c) =>
                f.Internet.Email(c.Nome.ToLower(), c.Sobrenome.ToLower()));


            return cliente.Generate(quantity);
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

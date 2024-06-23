using Features.Core;

namespace Features.Produtos
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Produto ObterPorEmail(string email);
    }
}
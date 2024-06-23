using System;
using System.Collections.Generic;

namespace Features.Produtos
{
    public interface IProdutoService : IDisposable
    {
        IEnumerable<Produto> ObterTodosAtivos();
        void Adicionar(Produto cliente);
        void Atualizar(Produto cliente);
        void Remover(Produto cliente);
        void Inativar(Produto cliente);
    }
}
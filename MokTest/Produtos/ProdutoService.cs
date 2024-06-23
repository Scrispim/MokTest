using System.Collections.Generic;
using System.Linq;
using MediatR;

namespace Features.Produtos
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediator _mediator;

        public ProdutoService(IProdutoRepository ProdutoRepository, 
                              IMediator mediator)
        {
            _produtoRepository = ProdutoRepository;
            _mediator = mediator;
        }

        public IEnumerable<Produto> ObterTodosAtivos()
        {
            return _produtoRepository.ObterTodos().Where(c => c.Ativo);
        }

        public void Adicionar(Produto Produto)
        {
            if (!Produto.EhValido())
                return;

            _produtoRepository.Adicionar(Produto);
            _mediator.Publish(new ProdutoEmailNotification("admin@me.com", Produto.EmailReponsavel, "Olá", "Bem vindo!"));
        }

        public void Atualizar(Produto Produto)
        {
            if (!Produto.EhValido())
                return;

            _produtoRepository.Atualizar(Produto);
            _mediator.Publish(new ProdutoEmailNotification("admin@me.com", Produto.EmailReponsavel, "Mudanças", "Dê uma olhada!"));
        }

        public void Inativar(Produto Produto)
        {
            if (!Produto.EhValido())
                return;

            Produto.Inativar();
            _produtoRepository.Atualizar(Produto);
            _mediator.Publish(new ProdutoEmailNotification("admin@me.com", Produto.EmailReponsavel, "Até breve", "Até mais tarde!"));
        }

        public void Remover(Produto Produto)
        {
            _produtoRepository.Remover(Produto.Id);
            _mediator.Publish(new ProdutoEmailNotification("admin@me.com", Produto.EmailReponsavel, "Adeus", "Tenha uma boa jornada!"));
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}
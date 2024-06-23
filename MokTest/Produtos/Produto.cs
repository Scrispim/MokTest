using System;
using Features.Core;
using FluentValidation;

namespace Features.Produtos
{
    public class Produto : Entity
    {
        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string EmailReponsavel { get; private set; }
        public bool Ativo { get; private set; }

        protected Produto()
        {
        }

        public Produto(Guid id, string codigo, string descricao, DateTime dataInicio, string email, bool ativo,
            DateTime dataCadastro)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
            DataInicio = dataInicio;
            EmailReponsavel = email;
            Ativo = ativo;
            DataCadastro = dataCadastro;
        }

        public string DescricaoCompleta()
        {
            return $"#{Codigo}: {Descricao}";
        }

        public bool EhEspecial()
        {
            return DataCadastro < DateTime.Now.AddYears(-3) && Ativo;
        }

        public void Inativar()
        {
            Ativo = false;
        }

        public override bool EhValido()
        {
            ValidationResult = new ProdutoValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ProdutoValidacao : AbstractValidator<Produto>
    {
        public ProdutoValidacao()
        {
            RuleFor(c => c.Codigo)
                .NotEmpty().WithMessage("Por favor, certifique-se de ter inserido o codigo")
                .Length(2, 100).WithMessage("O codigo deve ter entre 2 e 100 caracteres");

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("Por favor, certifique-se de ter inserido a descrição")
                .Length(2, 150).WithMessage("A descrição deve ter entre 2 e 150 caracteres");


            RuleFor(c => c.EmailReponsavel)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        public static bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-18);
        }
    }
}
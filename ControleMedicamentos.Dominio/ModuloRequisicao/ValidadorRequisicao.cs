using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class ValidadorRequisicao : AbstractValidator<Requisicao>
    {

        public ValidadorRequisicao()
        {
            RuleFor(x => x.QtdMedicamento)
                .GreaterThan(0)
                .WithMessage("O campo quantidade deve ser maior que 0");

            RuleFor(x => x.Data)
                 .NotEqual(DateTime.MinValue)
                 .WithMessage("O campo Data é obrigatório");

            RuleFor(x => x.Paciente.Nome)
                .NotNull().NotEmpty();

        }
    }
}

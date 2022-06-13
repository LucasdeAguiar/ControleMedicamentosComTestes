﻿using FluentValidation;
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
            RuleFor(x => x.Medicamento)
                .NotNull().NotEmpty();

            RuleFor(x => x.Paciente)
                .NotNull().NotEmpty();



        }
    }
}

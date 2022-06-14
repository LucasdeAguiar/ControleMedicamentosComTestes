using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloRequisicao
{
    [TestClass]
    public class RequisicaoTest
    {
        Paciente paciente = new Paciente("Lucas", "321654987");
        public RequisicaoTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void QuantidadeMedicamentoValido()
        {
            var requisition = new Requisicao();
            requisition.QtdMedicamento = 0;

            ValidadorRequisicao validador = new ValidadorRequisicao();
            var resultado = validador.Validate(requisition);

            Assert.AreEqual("O campo quantidade deve ser maior que 0", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void DataValida()
        {
            var requisition = new Requisicao();
            requisition.QtdMedicamento =  2;
            requisition.Data = DateTime.MinValue;

            ValidadorRequisicao validador = new ValidadorRequisicao();
            var resultado = validador.Validate(requisition);

            Assert.AreEqual("O campo Data é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void PacienteNomeValido()
        {
            Paciente p = new Paciente();
            var requisition = new Requisicao();
            requisition.QtdMedicamento = 2;
            requisition.Data = DateTime.MaxValue;
            requisition.Paciente = p;

            ValidadorRequisicao validador = new ValidadorRequisicao();
            var resultado = validador.Validate(requisition);

            Assert.AreEqual("'Paciente Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

    }
}

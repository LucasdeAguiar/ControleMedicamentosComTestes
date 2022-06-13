using ControleMedicamentos.Dominio.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloPaciente
{
    [TestClass]
    public class PacienteTest
    {
        public PacienteTest() {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]

        public void NomeValido()
        {
            Paciente paciente = new Paciente("" , "123");

            ValidadorPaciente validadorPaciente = new ValidadorPaciente();

            var resultado1 = validadorPaciente.Validate(paciente);

            Assert.AreEqual("Deve ser inserido um nome", resultado1.Errors[0].ErrorMessage);
        }

        public void cartaoSUSDigitosValidos()
        {
            Paciente paciente = new Paciente("Lucas", "123");

            ValidadorPaciente validadorPaciente = new ValidadorPaciente();

            var resultado1 = validadorPaciente.Validate(paciente);

            Assert.AreEqual("O cartão do SUS deve ter pelo menos 8 dígitos", resultado1.Errors[0].ErrorMessage);
        }

        public void cartaoSusContemSoNumero()
        {
            Paciente paciente = new Paciente("Lucas", "12345abc");

            ValidadorPaciente validadorPaciente = new ValidadorPaciente();

            var resultado1 = validadorPaciente.Validate(paciente);

            Assert.AreEqual("O cartão do SUS só pode conter números", resultado1.Errors[0].ErrorMessage);
        }
    }
}

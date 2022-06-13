using ControleMedicamentos.Dominio.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloFuncionario
{
    [TestClass]
    public class FuncionarioTest
    {
        public FuncionarioTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]

        public void NomeValido()
        {
            Funcionario funcionario = new Funcionario("", "Joao123@gmail.com", "joao123");

            ValidadorFuncionario validadorPaciente = new ValidadorFuncionario();

            var resultado1 = validadorPaciente.Validate(funcionario);

            Assert.AreEqual("Deve ser inserido um nome", resultado1.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void LoginValido()
        {
            Funcionario funcionario = new Funcionario("João", "", "joao123");

            ValidadorFuncionario validadorPaciente = new ValidadorFuncionario();

            var resultado1 = validadorPaciente.Validate(funcionario);

            Assert.AreEqual("Deve conter um email válido", resultado1.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void SenhaValida()
        {
            Funcionario funcionario = new Funcionario("João", "Joao123@gmail.com", "joao123");


            ValidadorFuncionario validadorPaciente = new ValidadorFuncionario();

            var resultado1 = validadorPaciente.Validate(funcionario);

            Assert.AreEqual("A senha deve conter pelo menos 8 caracteres", resultado1.Errors[0].ErrorMessage);
        }
    }
}

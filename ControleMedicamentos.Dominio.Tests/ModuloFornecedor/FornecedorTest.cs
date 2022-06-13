using ControleMedicamentos.Dominio.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloFornecedor
{
    [TestClass]

    public class FornecedorTest
    {
        public FornecedorTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void NomeValido()
        {
            Fornecedor funcionario = new Fornecedor("", "123456789", "ribamar@vascao.com","Rio de Janeiro","Rio de Janeiro");

            ValidadorFornecedor validadorFornecedor = new ValidadorFornecedor();

            var resultado1 = validadorFornecedor.Validate(funcionario);

            Assert.AreEqual("Deve ser inserido um nome", resultado1.Errors[0].ErrorMessage);
        }


        [TestMethod]

        public void telefoneValido()
        {
            Fornecedor fornecedor = new Fornecedor("Ribamar", "12345678", "ribamar@vascao.com", "Rio de Janeiro", "Rio de Janeiro");

            ValidadorFornecedor validadorFornecedor = new ValidadorFornecedor();

            var resultado1 = validadorFornecedor.Validate(fornecedor);

            Assert.AreEqual("Deve ser inserido um telefone com no mínimo 9 caracteres", resultado1.Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void EmailValido()
        {
            Fornecedor fornecedor = new Fornecedor("Ribamar", "123456789", "ribamar.", "Rio de Janeiro", "Rio de Janeiro");

            ValidadorFornecedor validadorFornecedor = new ValidadorFornecedor();

            var resultado1 = validadorFornecedor.Validate(fornecedor);

            Assert.AreEqual("Deve conter um email válido", resultado1.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void CidadeValida()
        {
            Fornecedor fornecedor = new Fornecedor("Ribamar", "123456789", "ribamar@vascao.com.", "Rio", "Rio de Janeiro");

            ValidadorFornecedor validadorFornecedor = new ValidadorFornecedor();

            var resultado1 = validadorFornecedor.Validate(fornecedor);

            Assert.AreEqual("A cidade deve conter pelo menos 4 caracteres", resultado1.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void EstadoValido()
        {
            Fornecedor fornecedor = new Fornecedor("Ribamar", "123456789", "ribamar@vascao.com.", "Rio de Janeiro", "Rj");

            ValidadorFornecedor validadorFornecedor = new ValidadorFornecedor();

            var resultado1 = validadorFornecedor.Validate(fornecedor);

            Assert.AreEqual("O estado deve conter pelo menos 3 caracteres", resultado1.Errors[0].ErrorMessage);
        }
    }
}

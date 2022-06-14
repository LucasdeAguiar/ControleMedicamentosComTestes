using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class MedicamentoTest
    {
        List<Requisicao> requisicoes = new List<Requisicao>();
        Fornecedor fornecedor = new Fornecedor("Ribamar", "123456789", "ribamar@vascao.com", "Rio de Janeiro", "Rio de Janeiro");
        public MedicamentoTest()
        {
            
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }


        [TestMethod]
        public void NomeValido()
        {
            var medicamento = new Medicamento();
            medicamento.Nome = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void DescricaoValida()
        {
            var medicamento = new Medicamento();
            medicamento.Nome = "Nome teste";
            medicamento.Descricao = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("'Descricao' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void QuantidadeValida()
        {

            Medicamento medicamento = new Medicamento("Paracetamol", "Dor", "Lote 40", DateTime.Now, requisicoes, fornecedor);


            ValidadorMedicamento validadorPaciente = new ValidadorMedicamento();

            var resultado1 = validadorPaciente.Validate(medicamento);

            Assert.AreEqual("Quantidade deve conter mais do que 0", resultado1.Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void QuantidadeDisponivelValida()
        {
            var medicamento = new Medicamento();
            medicamento.Nome = "Rivotril";
            medicamento.Descricao = "Dor de cabeça";
            medicamento.Lote = "Lote 14";
            medicamento.Validade = DateTime.Now;
            medicamento.QuantidadeDisponivel = 0;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("Quantidade deve conter mais do que 0", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void LoteValida()
        {
            var medicamento = new Medicamento();
            medicamento.Nome = "Rivotril";
            medicamento.Descricao = "Dor de cabeça";
            medicamento.Lote = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("'Lote' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void ValidaValidade()
        {
            var medicamento = new Medicamento();
            medicamento.Nome = "Rivotril";
            medicamento.Descricao = "Dor de cabeça";
            medicamento.Lote = "Lote 14";
            medicamento.Validade = DateTime.MinValue;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("O campo Validade é obrigatório", resultado.Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void ValidaFornecedor()
        {
            var medicamento = new Medicamento();
            medicamento.Nome = "Rivotril";
            medicamento.Descricao = "Dor de cabeça";
            medicamento.Lote = "Lote 14";
            medicamento.Validade = DateTime.Now;
            medicamento.QuantidadeDisponivel = 15;
            medicamento.Fornecedor = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("'Fornecedor' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }


    }
}

using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFornecedor
{
    [TestClass]
    public class RepositorioFornecedorEmBancoDadosTest
    {
        private Fornecedor fornecedor;
        private RepositorioFornecedorEmBancoDados repositorio;

        public RepositorioFornecedorEmBancoDadosTest()
        {
            //Db.ExecutarSql("DELETE FROM TBPACIENTE; DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)");

            fornecedor = new Fornecedor("Ribamar", "123456789", "ribamar@vascao.com.", "Rio de Janeiro", "Rio De Janeiro");
            repositorio = new RepositorioFornecedorEmBancoDados();
        }

        [TestMethod]
        public void Deve_inserir_novo_fornecedor()
        {
            //action
            repositorio.Inserir(fornecedor);

            //assert
            var fornecedorEncontrado = repositorio.SelecionarPorNumero(fornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor.Telefone, fornecedorEncontrado.Telefone);
            Assert.AreEqual(fornecedor.Email, fornecedorEncontrado.Email);
            Assert.AreEqual(fornecedor.Cidade, fornecedorEncontrado.Cidade);
            Assert.AreEqual(fornecedor.Estado, fornecedorEncontrado.Estado);
        }

        [TestMethod]
        public void Deve_editar_Fornecedor()
        {
            //arrange                      
            repositorio.Inserir(fornecedor);

            //action

            fornecedor.Nome = "Lucas";
            fornecedor.Email = "lucasaguiar@outlook.com";
            fornecedor.Telefone = "987654321";
            fornecedor.Cidade = "São Paulo";
            fornecedor.Estado = "São Paulo";
            repositorio.Editar(fornecedor);

            //assert
            var fornecedorEncontrado = repositorio.SelecionarPorNumero(fornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor.Nome, fornecedorEncontrado.Nome);
            Assert.AreEqual(fornecedor.Email, fornecedorEncontrado.Email);
            Assert.AreEqual(fornecedor.Telefone, fornecedorEncontrado.Telefone);
            Assert.AreEqual(fornecedor.Cidade, fornecedorEncontrado.Cidade);
            Assert.AreEqual(fornecedor.Estado, fornecedorEncontrado.Estado);
        }

        [TestMethod]
        public void Deve_excluir_Fornecedor()
        {
            //arrange           
            repositorio.Inserir(fornecedor);

            //action           
            repositorio.Excluir(fornecedor);

            //assert
            var pacienteEncontrado = repositorio.SelecionarPorNumero(fornecedor.Id);
            Assert.IsNull(pacienteEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_um_fornecedor()
        {
            //arrange          
            repositorio.Inserir(fornecedor);

            //action
            var fornecedorEncontrado = repositorio.SelecionarPorNumero(fornecedor.Id);

            //assert
            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor.Nome, fornecedorEncontrado.Nome);
        }

        //OBS: CASO O MÉTODO SEJA EXECUTADO COM DADOS JÁ EXISTENTES NO BANCO,
        //LOGO, O TESTE NÃO PASSARÁ..
        [TestMethod]
        public void Deve_selecionar_todos_um_fornecedor()
        {



            //arrange
            var p01 = new Fornecedor("Ribamar", "123456789", "ribamar@vascao.com.", "Rio de Janeiro", "Rio de Janeiro");
            var p02 = new Fornecedor("Nenê", "123456789", "nene@vascao.com.", "Rio de Janeiro", "Rio de Janeiro");
            var p03 = new Fornecedor("Gabigalo","123456789", "gabigol@flamingo.com", "Rio de Janeiro", "Rio De Janeiro");

            var repositorio = new RepositorioFornecedorEmBancoDados();
            repositorio.Inserir(p01);
            repositorio.Inserir(p02);
            repositorio.Inserir(p03);



            //action
            var fornecedor = repositorio.SelecionarTodos();

            //assert

            Assert.AreEqual(3, fornecedor.Count);

            Assert.AreEqual(p01.Nome, fornecedor[0].Nome);
            Assert.AreEqual(p02.Nome, fornecedor[1].Nome);
            Assert.AreEqual(p03.Nome, fornecedor[2].Nome);
        }
    }
}

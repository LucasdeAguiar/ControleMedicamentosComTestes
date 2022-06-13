using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioEmBancoDadosTest
    {
        private Funcionario funcionario;
        private RepositorioFuncionarioEmBancoDados repositorio;

        public RepositorioFuncionarioEmBancoDadosTest()
        {
            //Db.ExecutarSql("DELETE FROM TBPACIENTE; DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)");

            funcionario = new Funcionario("Lucas", "lucasaguiar@gmail.com", "lucas123");
            repositorio = new RepositorioFuncionarioEmBancoDados();
        }

        [TestMethod]
        public void Deve_inserir_novo_funcionario()
        {
            //action
            repositorio.Inserir(funcionario);

            //assert
            var funcionarioEncontrado = repositorio.SelecionarPorNumero(funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario.Nome, funcionarioEncontrado.Nome);
            Assert.AreEqual(funcionario.Login, funcionarioEncontrado.Login);
            Assert.AreEqual(funcionario.Senha, funcionarioEncontrado.Senha);
        }


        [TestMethod]
        public void Deve_editar_Funcionario()
        {
            //arrange                      
            repositorio.Inserir(funcionario);

            //action

            funcionario.Nome = "Lucas";
            funcionario.Login = "lucasaguiar@outlook.com";
            funcionario.Senha = "lucas123";
            repositorio.Editar(funcionario);

            //assert
            var funcionarioEncontrado = repositorio.SelecionarPorNumero(funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario.Nome, funcionarioEncontrado.Nome);
            Assert.AreEqual(funcionario.Login, funcionarioEncontrado.Login);
            Assert.AreEqual(funcionario.Senha, funcionarioEncontrado.Senha);
        }

        [TestMethod]
        public void Deve_excluir_Funcionario()
        {
            //arrange           
            repositorio.Inserir(funcionario);

            //action           
            repositorio.Excluir(funcionario);

            //assert
            var pacienteEncontrado = repositorio.SelecionarPorNumero(funcionario.Id);
            Assert.IsNull(pacienteEncontrado);
        }


        [TestMethod]
        public void Deve_selecionar_apenas_um_funcionario()
        {
            //arrange          
            repositorio.Inserir(funcionario);

            //action
            var pacienteEncontrado = repositorio.SelecionarPorNumero(funcionario.Id);

            //assert
            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(funcionario, pacienteEncontrado);
        }

        //OBS: CASO O MÉTODO SEJA EXECUTADO COM DADOS JÁ EXISTENTES NO BANCO,
        //LOGO, O TESTE NÃO PASSARÁ..
        [TestMethod]
        public void Deve_selecionar_todos_um_funcionario()
        {

            

            //arrange
            var p01 = new Funcionario("Ribamar", "hojeTemGolDoRibas@vascaoDaMassa.com", "12345678");
            var p02 = new Funcionario("Rodinei", "ruimdinei@flaDaDepressao.com.br", "12345678");
            var p03 = new Funcionario("Léo Pereira", "leoPereba@flamingo.com", "12345678");

            var repositorio = new RepositorioFuncionarioEmBancoDados();
            repositorio.Inserir(p01);
            repositorio.Inserir(p02);
            repositorio.Inserir(p03);



            //action
            var funcionarios = repositorio.SelecionarTodos();

            //assert

            Assert.AreEqual(3, funcionarios.Count);

            Assert.AreEqual(p01.Nome, funcionarios[0].Nome);
            Assert.AreEqual(p02.Nome, funcionarios[1].Nome);
            Assert.AreEqual(p03.Nome, funcionarios[2].Nome);
        }
    }
}

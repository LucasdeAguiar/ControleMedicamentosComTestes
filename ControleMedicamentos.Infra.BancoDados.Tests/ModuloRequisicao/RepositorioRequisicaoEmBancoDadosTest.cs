using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloRequisicao
{
    [TestClass]
    public class RepositorioRequisicaoEmBancoDadosTest
    {


        //Por algum motivo , está dando conflito nas fks no banco de dados, até o momento sem encontrar o motivo do erro
        //Resultado: Afetando todos os testes realizaddos nas classes
       
        public RepositorioRequisicaoEmBancoDadosTest()
        {

           
           
        }

        [TestMethod]
        public void Deve_inserir_requisicao()
        {
            
            Funcionario funcionario = new Funcionario("Lucas", "lucas@gmail.com", "lucas123");
            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
            repositorioFuncionario.Inserir(funcionario);

            Paciente paciente = new Paciente("Lucas", "123");
            var repositorioPaciente = new RepositorioPacienteEmBancoDados();
            repositorioPaciente.Inserir(paciente);

            
            var fornecedor = new Fornecedor("Ribamar", "123456789", "ribamar@vascao.com", "Rio de Janeiro", "Rio de Janeiro");
            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFornecedor.Inserir(fornecedor);

            Medicamento med = new Medicamento("Rivotril", "Dor de cabeça", "Lote 14", DateTime.Now.Date, 15, fornecedor);
            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioMedicamento.Inserir(med);
         

            Requisicao requisition = new Requisicao(med, paciente, 5, DateTime.Now.Date, funcionario);
            var repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();
            repositorioRequisicao.Inserir(requisition);

            Requisicao requisicaoEncontrada = repositorioRequisicao.SelecionarPorNumero(requisition.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisition.Id, requisicaoEncontrada.Id);
            Assert.AreEqual(requisition.Funcionario.Id, requisicaoEncontrada.Funcionario.Id);
            Assert.AreEqual(requisition.Paciente.Id, requisicaoEncontrada.Paciente.Id);
            Assert.AreEqual(requisition.Medicamento.Id, requisicaoEncontrada.Medicamento.Id);
          
        }

        [TestMethod]
        public void Deve_editar_requisicao()
        {
            Funcionario funcionario = new Funcionario("Lucas", "lucas@gmail.com", "lucas123");
            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
            repositorioFuncionario.Inserir(funcionario);

            Paciente paciente = new Paciente("Lucas", "123");
            var repositorioPaciente = new RepositorioPacienteEmBancoDados();
            repositorioPaciente.Inserir(paciente);


            var fornecedor = new Fornecedor("Ribamar", "123456789", "ribamar@vascao.com", "Rio de Janeiro", "Rio de Janeiro");
            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFornecedor.Inserir(fornecedor);

            Medicamento med = new Medicamento("Rivotril", "Dor de cabeça", "Lote 14", DateTime.Now.Date, 15, fornecedor);
            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioMedicamento.Inserir(med);


            Requisicao req2 = new Requisicao(med, paciente, 5, DateTime.Now.Date, funcionario);
            var repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();
            repositorioRequisicao.Inserir(req2);

            Requisicao requisition = repositorioRequisicao.SelecionarPorNumero(req2.Id);
            requisition.QtdMedicamento = 15;
            repositorioRequisicao.Editar(requisition);

            Requisicao requisicaoEncontrada = repositorioRequisicao.SelecionarPorNumero(req2.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisition.Id, requisicaoEncontrada.Id);
            Assert.AreEqual(requisition.Funcionario.Id, requisicaoEncontrada.Funcionario.Id);
            Assert.AreEqual(requisition.Paciente.Id, requisicaoEncontrada.Paciente.Id);
            Assert.AreEqual(requisition.Medicamento.Id, requisicaoEncontrada.Medicamento.Id);
     
        }

        [TestMethod]
        public void Deve_excluir_requisicao()
        {
            Funcionario funcionario = new Funcionario("Lucas", "lucas@gmail.com", "lucas123");
            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
            repositorioFuncionario.Inserir(funcionario);

            Paciente paciente = new Paciente("Lucas", "123");
            var repositorioPaciente = new RepositorioPacienteEmBancoDados();
            repositorioPaciente.Inserir(paciente);


            var fornecedor = new Fornecedor("Ribamar", "123456789", "ribamar@vascao.com", "Rio de Janeiro", "Rio de Janeiro");
            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFornecedor.Inserir(fornecedor);

            Medicamento med = new Medicamento("Rivotril", "Dor de cabeça", "Lote 14", DateTime.Now.Date, 15, fornecedor);
            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioMedicamento.Inserir(med);

            Requisicao requisition = new Requisicao(med, paciente, 5, DateTime.Now.Date, funcionario);
            var repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();
            

            //arrange           
            repositorioRequisicao.Inserir(requisition);

            //action           
            repositorioRequisicao.Excluir(requisition);

            //assert
            var requisicaoEncontrado = repositorioRequisicao.SelecionarPorNumero(requisition.Id);
            Assert.IsNull(requisicaoEncontrado);
        }


        [TestMethod]
        public void Deve_selecionar_um_requisicao()
        {

            Funcionario funcionario = new Funcionario("Lucas", "lucas@gmail.com", "lucas123");
            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
            repositorioFuncionario.Inserir(funcionario);

            Paciente paciente = new Paciente("Lucas", "123");
            var repositorioPaciente = new RepositorioPacienteEmBancoDados();
            repositorioPaciente.Inserir(paciente);


            var fornecedor = new Fornecedor("Ribamar", "123456789", "ribamar@vascao.com", "Rio de Janeiro", "Rio de Janeiro");
            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFornecedor.Inserir(fornecedor);

            Medicamento med = new Medicamento("Rivotril", "Dor de cabeça", "Lote 14", DateTime.Now.Date, 15, fornecedor);
            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioMedicamento.Inserir(med);

            Requisicao requisition = new Requisicao(med, paciente, 5, DateTime.Now.Date, funcionario);
            var repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();

            Requisicao requisicaoEncontrada = repositorioRequisicao.SelecionarPorNumero(requisition.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisition.Id, requisicaoEncontrada.Id);
         
        }

        //OBS: CASO O MÉTODO SEJA EXECUTADO COM DADOS JÁ EXISTENTES NO BANCO,
        //LOGO, O TESTE NÃO PASSARÁ..

        [TestMethod]
        public void Deve_selecionar_um_requisicoes()
        {

            Funcionario funcionario = new Funcionario("Lucas", "lucas@gmail.com", "lucas123");
            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
            repositorioFuncionario.Inserir(funcionario);

            Paciente paciente = new Paciente("Lucas", "123");
            var repositorioPaciente = new RepositorioPacienteEmBancoDados();
            repositorioPaciente.Inserir(paciente);


            var fornecedor = new Fornecedor("Ribamar", "123456789", "ribamar@vascao.com", "Rio de Janeiro", "Rio de Janeiro");
            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFornecedor.Inserir(fornecedor);

            Medicamento med = new Medicamento("Rivotril", "Dor de cabeça", "Lote 14", DateTime.Now.Date, 15, fornecedor);
            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioMedicamento.Inserir(med);


            var repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();

            Requisicao requisition1 = new Requisicao(med, paciente, 5, DateTime.Now.Date, funcionario);
            repositorioRequisicao.Inserir(requisition1);

            Requisicao requisition2 = new Requisicao(med, paciente, 10, DateTime.Now.Date, funcionario);
            repositorioRequisicao.Inserir(requisition1);


            var requisicoes = repositorioRequisicao.SelecionarTodos();

            Assert.AreEqual(2, requisicoes.Count);

      
          
        }

    }
}

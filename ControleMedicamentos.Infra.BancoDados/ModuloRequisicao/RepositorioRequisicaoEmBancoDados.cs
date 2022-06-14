using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class RepositorioRequisicaoEmBancoDados
    {
        RepositorioFuncionarioEmBancoDados repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
        RepositorioPacienteEmBancoDados repositorioPaciente = new RepositorioPacienteEmBancoDados();
        RepositorioMedicamentoEmBancoDados repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();

        private const string enderecoBanco =
              "Data Source=(LocalDB)\\MSSqlLocalDB;" +
              "Initial Catalog=ControleDeMedicamentosDB;" +
              "Integrated Security=True;" +
              "Pooling=False";

        #region SQL Queries
        private const string sqlInserir =
            @"INSERT INTO [TBREQUISICAO]
                (
                    [FUNCIONARIO_ID],       
                    [PACIENTE_ID], 
                    [MEDICAMENTO_ID],
                    [QUANTIDADEMEDICAMENTO],                    
                    [DATA]      
                )
            VALUES
                (
                    @FUNCIONARIO_ID,
                    @PACIENTE_ID,
                    @MEDICAMENTO_ID,
                    @QUANTIDADEMEDICAMENTO,
                    @DATA
                ); SELECT SCOPE_IDENTITY();";


        private const string sqlEditar =
            @" UPDATE [TBREQUISICAO]
                    SET 
                        [FUNCIONARIO_ID] = @FUNCIONARIO_ID, 
                        [PACIENTE_ID] = @PACIENTE_ID, 
                        [MEDICAMENTO_ID] = @MEDICAMENTO_ID,
                        [QUANTIDADEMEDICAMENTO] = @QUANTIDADEMEDICAMENTO, 
                        [DATA] = @DATA
                    WHERE [ID] = @ID";


        private const string sqlExcluir =
           @"DELETE FROM [TBREQUISICAO] 
                WHERE [ID] = @ID";

        private const string sqlSelecionarTodos =
            @"SELECT 
                REQUISICAO.[ID],       
                REQUISICAO.[FUNCIONARIO_ID],
                REQUISICAO.[PACIENTE_ID],
                REQUISICAO.[MEDICAMENTO_ID],             
                REQUISICAO.[QUANTIDADEMEDICAMENTO], 
                REQUISICAO.[DATA],    
                FUNCIONARIO.[NOME],              
                FUNCIONARIO.[LOGIN],                    
                FUNCIONARIO.[SENHA], 
                PACIENTE.[NOME],
                PACIENTE.[CARTAOSUS],
                MEDICAMENTO.[NOME],
                MEDICAMENTO.[DESCRICAO],
                MEDICAMENTO.[LOTE],
                MEDICAMENTO.[VALIDADE],
                MEDICAMENTO.[QUANTIDADEDISPONIVEL]
            FROM
                [TBREQUISICAO] AS REQUISICAO INNER JOIN [TBFUNCIONARIO] AS FUNCIONARIO
                    ON REQUISICAO.FUNCIONARIO_ID = FUNCIONARIO.ID
                INNER JOIN [TBPACIENTE] AS PACIENTE 
                    ON REQUISICAO.PACIENTE_ID = PACIENTE.ID
                INNER JOIN [TBMEDICAMENTO] AS MEDICAMENTO
                    ON REQUISICAO.MEDICAMENTO_ID = MEDICAMENTO.ID";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
                REQUISICAO.[ID],       
                REQUISICAO.[FUNCIONARIO_ID],
                REQUISICAO.[PACIENTE_ID],
                REQUISICAO.[MEDICAMENTO_ID],             
                REQUISICAO.[QUANTIDADEMEDICAMENTO],      
                REQUISICAO.[DATA],   
                FUNCIONARIO.[NOME],              
                FUNCIONARIO.[LOGIN],                    
                FUNCIONARIO.[SENHA], 
                PACIENTE.[NOME],
                PACIENTE.[CARTAOSUS],
                MEDICAMENTO.[NOME],
                MEDICAMENTO.[DESCRICAO],
                MEDICAMENTO.[LOTE],
                MEDICAMENTO.[VALIDADE],
                MEDICAMENTO.[QUANTIDADEDISPONIVEL]
            FROM
                [TBREQUISICAO] AS REQUISICAO INNER JOIN [TBFUNCIONARIO] AS FUNCIONARIO
                    ON REQUISICAO.FUNCIONARIO_ID = FUNCIONARIO.ID
                INNER JOIN [TBPACIENTE] AS PACIENTE 
                    ON REQUISICAO.PACIENTE_ID = PACIENTE.ID
                INNER JOIN [TBMEDICAMENTO] AS MEDICAMENTO
                    ON REQUISICAO.MEDICAMENTO_ID = MEDICAMENTO.ID
            WHERE
                REQUISICAO.[ID] = @ID";


        #endregion



        public ValidationResult Inserir(Requisicao novoRegistro)
        {
            var validador = new ValidadorRequisicao();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosRequisicao(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoRegistro.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Requisicao requisicao)
        {
            var validador = new ValidadorRequisicao();

            var resultadoValidacao = validador.Validate(requisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosRequisicao(requisicao, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Requisicao requisicao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", requisicao.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }


        public List<Requisicao> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            List<Requisicao> requisicoes = new List<Requisicao>();

            while (leitorRequisicao.Read())
            {
                Requisicao medicamento = ConverterParaRequisicao(leitorRequisicao);

                requisicoes.Add(medicamento);
            }

            conexaoComBanco.Close();

            return requisicoes;
        }


        public Requisicao SelecionarPorNumero(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            Requisicao requisicao = null;
            if (leitorRequisicao.Read())
                requisicao = ConverterParaRequisicao(leitorRequisicao);

            conexaoComBanco.Close();

            return requisicao;
        }

        private void ConfigurarParametrosRequisicao(Requisicao requisicao, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", requisicao.Id);
            comando.Parameters.AddWithValue("FUNCIONARIO_ID", requisicao.Funcionario.Id);
            comando.Parameters.AddWithValue("PACIENTE_ID", requisicao.Paciente.Id);
            comando.Parameters.AddWithValue("MEDICAMENTO_ID", requisicao.Medicamento.Id);
            comando.Parameters.AddWithValue("QUANTIDADEMEDICAMENTO", requisicao.QtdMedicamento);
            comando.Parameters.AddWithValue("DATA", requisicao.Data);
        }


        private Requisicao ConverterParaRequisicao(SqlDataReader leitorRequisicao)
        {

            var id = Convert.ToInt32(leitorRequisicao["ID"]);
            var funcionarioId = Convert.ToInt32(leitorRequisicao["FUNCIONARIO_ID"]);
            var pacienteId = Convert.ToInt32(leitorRequisicao["PACIENTE_ID"]);
            var medicamentoId = Convert.ToInt32(leitorRequisicao["MEDICAMENTO_ID"]);
            var quantidadeMedicamento = Convert.ToInt32(leitorRequisicao["QUANTIDADEMEDICAMENTO"]);
            var data = Convert.ToDateTime(leitorRequisicao["DATA"]);

            Requisicao requisicao = new Requisicao();
            requisicao.Id = id;
            requisicao.QtdMedicamento = quantidadeMedicamento;
            requisicao.Data = data;


            requisicao.Funcionario = repositorioFuncionario.SelecionarPorNumero(funcionarioId);
            requisicao.Paciente = repositorioPaciente.SelecionarPorNumero(pacienteId);
            requisicao.Medicamento = repositorioMedicamento.SelecionarPorNumero(medicamentoId);

            return requisicao;
        }
    }
}

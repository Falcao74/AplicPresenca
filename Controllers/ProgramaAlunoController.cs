using ListaPresencaAPP.Helpers;
using ListaPresencaAPP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaPresencaAPP.Controllers
{
    public class ProgramaAlunoController : EntidadeBase<ProgramaAluno>
    {
        public override void Adicionar(ProgramaAluno entidade)
        {
            string query = "INSERT INTO ProgramaAluno (ProgramaId, AlunoMatricula, Ativo) " +
                           "VALUES (@ProgramaId, @AlunoMatricula, @Ativo);";

            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@ProgramaId", entidade.ProgramaId);
                comando.Parameters.AddWithValue("@AlunoMatricula", entidade.AlunoMatricula);
                comando.Parameters.AddWithValue("@Ativo", entidade.Ativo);
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao adicionar o registro na tabela ProgramaAluno: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public override void Atualizar(ProgramaAluno entidade)
        {
            string query = "UPDATE ProgramaAluno SET Ativo = @Ativo " +
                           "WHERE ProgramaId = @ProgramaId AND AlunoMatricula = @AlunoMatricula;";

            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@Ativo", entidade.Ativo);
                comando.Parameters.AddWithValue("@ProgramaId", entidade.ProgramaId);
                comando.Parameters.AddWithValue("@AlunoMatricula", entidade.AlunoMatricula);
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao atualizar o registro na tabela ProgramaAluno: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Desativar(int programaId, int alunoMatricula)
        {
            string query = "UPDATE ProgramaAluno SET Ativo = 0 " +
                           "WHERE ProgramaId = @ProgramaId AND AlunoMatricula = @AlunoMatricula;";

            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@ProgramaId", programaId);
                comando.Parameters.AddWithValue("@AlunoMatricula", alunoMatricula);
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao desativar o registro na tabela ProgramaAluno: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        // Método para obter um registro da tabela ProgramaAluno com base no ProgramaId e AlunoMatricula.
        public ProgramaAluno ObterPorId(int programaId, int alunoMatricula)
        {
            ProgramaAluno programaAluno = new();
            string query = "SELECT ProgramaId, AlunoMatricula, Ativo " +
                           "FROM ProgramaAluno " +
                           "WHERE ProgramaId = @ProgramaId AND AlunoMatricula = @AlunoMatricula;";

            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@ProgramaId", programaId);
                comando.Parameters.AddWithValue("@AlunoMatricula", alunoMatricula);
                conexao.Open();
                using SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    programaAluno = new()
                    {
                        ProgramaId = reader.GetInt32(0),
                        AlunoMatricula = reader.GetInt32(1),
                        Ativo = reader.GetBoolean(2)
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao obter o registro da tabela ProgramaAluno: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return programaAluno;
        }

        public override ProgramaAluno ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<int> ListarMatriculasAlunosAtivosPorPrograma(int programaId)
        {
            List<int> matriculasAlunosAtivos = new();
            string query = "SELECT AlunoMatricula " +
                           "FROM ProgramaAluno " +
                           "WHERE ProgramaId = @ProgramaId AND Ativo = 1;";

            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@ProgramaId", programaId);
                conexao.Open();
                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    matriculasAlunosAtivos.Add(reader.GetInt32(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar as matrículas dos alunos ativos por programa: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return matriculasAlunosAtivos;
        }
    }

}

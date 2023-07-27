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
    internal class ProfessorController : EntidadeBase<Professor>
    {
        public override void Adicionar(Professor entidade)
        {
            string query = "INSERT INTO Professores (Nome, Email, Titulo, Ativo) VALUES (@Nome, @Email, @Titulo, @Ativo)";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@Nome", entidade.Nome);
                comando.Parameters.AddWithValue("@Email", entidade.Email);
                comando.Parameters.AddWithValue("@Nascimento", entidade.Titulo);
                comando.Parameters.AddWithValue("@Ativo", entidade.Ativo);
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao adicionar o professor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public override void Atualizar(Professor entidade)
        {
            string query = "UPDATE Professores SET Nome = @Nome, Email = @Email, Titulo = @Titulo, Ativo = @Ativo WHERE ProfessorId = @ProfessorId";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@Nome", entidade.Nome);
                comando.Parameters.AddWithValue("@Email", entidade.Email);
                comando.Parameters.AddWithValue("@Titulo", entidade.Titulo);
                comando.Parameters.AddWithValue("@Ativo", entidade.Ativo);
                comando.Parameters.AddWithValue("@ProfessorId", entidade.ProfessorId);
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao atualizar o professor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public override void Deletar(int id)
        {
            string query = "UPDATE Professores SET Ativo = 0 WHERE ProfessorId = @ProfessorId";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@ProfessorId", id);
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao deletar o professor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public override Professor ObterPorId(int id)
        {
            Professor professor = new();
            string query = "SELECT ProfessorId, Nome, Email, Titulo, Ativo FROM Professores WHERE ProfessorId = @ProfessorId";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@ProfessorId", id);
                conexao.Open();
                using SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    professor = new()
                    {
                        ProfessorId = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Email = reader.GetString(2),
                        Titulo = reader.GetString(3),
                        Ativo = reader.GetBoolean(4)
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao obter o professor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return professor;
        }
        public List<Professor> ListarProfessoresAtivos()
        {
            List<Professor> professoresAtivos = new();
            string query = "SELECT * FROM Professores WHERE Ativo = 1;";
            try
            {
                using SqlCommand comando = new(query, conexao);
                conexao.Open();
                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Professor professor = new()
                    {
                        ProfessorId = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Email = reader.GetString(2),
                        Titulo = reader.GetString(3),
                        Ativo = reader.GetBoolean(4)
                    };
                    professoresAtivos.Add(professor);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar os professores ativos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return professoresAtivos;
        }
        public List<Professor> ListarProfessoresInativos()
        {
            List<Professor> professoresInativos = new();
            string query = "SELECT * FROM Professores WHERE Ativo = 0;";
            try
            {
                using SqlCommand comando = new(query, conexao);
                conexao.Open();
                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Professor professor = new()
                    {
                        ProfessorId = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Email = reader.GetString(2),
                        Titulo = reader.GetString(3),
                        Ativo = reader.GetBoolean(4)
                    };
                    professoresInativos.Add(professor);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar os professores inativos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return professoresInativos;
        }
        public List<Professor> ListarTodosProfessores()
        {
            List<Professor> todosProfessores = new();
            string query = "SELECT * FROM Professores;";
            try
            {
                using SqlCommand comando = new(query, conexao);
                conexao.Open();
                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Professor professor = new()
                    {
                        ProfessorId = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Email = reader.GetString(2),
                        Titulo = reader.GetString(3),
                        Ativo = reader.GetBoolean(4)
                    };
                    todosProfessores.Add(professor);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar todos os professores: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return todosProfessores;
        }
        public List<Professor> ListarProfessoresAtivosDoPrograma(int programaId)
        {
            List<Professor> professoresAtivos = new();
            string query = "SELECT P.ProfessorId, P.Nome, P.Email, P.Titulo, P.Ativo " +
                           "FROM Professores P " +
                           "INNER JOIN ProgramaProfessor PP ON P.ProfessorId = PP.ProfessorId " +
                           "WHERE PP.ProgramaId = @ProgramaId AND P.Ativo = 1;";

            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@ProgramaId", programaId);
                conexao.Open();
                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    professoresAtivos.Add(new Professor
                    {
                        ProfessorId = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Email = reader.GetString(2),
                        Titulo = reader.GetString(3),
                        Ativo = reader.GetBoolean(4)
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar os professores ativos do programa: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return professoresAtivos;
        }
        public List<Professor> ListarProfessoresInativosDoPrograma(int programaId)
        {
            List<Professor> professoresInativos = new List<Professor>();
            string query = "SELECT P.ProfessorId, P.Nome, P.Email, P.Titulo, P.Ativo " +
                           "FROM Professores P " +
                           "INNER JOIN ProgramaProfessor PP ON P.ProfessorId = PP.ProfessorId " +
                           "WHERE PP.ProgramaId = @ProgramaId AND P.Ativo = 0;";

            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@ProgramaId", programaId);
                conexao.Open();
                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    professoresInativos.Add(new Professor
                    {
                        ProfessorId = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Email = reader.GetString(2),
                        Titulo = reader.GetString(3),
                        Ativo = reader.GetBoolean(4)
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar os professores inativos do programa: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return professoresInativos;
        }
        public List<Professor> ListarTodosProfessoresDoPrograma(int programaId)
        {
            List<Professor> todosProfessores = new();
            string query = "SELECT P.ProfessorId, P.Nome, P.Email, P.Titulo, P.Ativo " +
                           "FROM Professores P " +
                           "INNER JOIN ProgramaProfessor PP ON P.ProfessorId = PP.ProfessorId " +
                           "WHERE PP.ProgramaId = @ProgramaId;";

            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@ProgramaId", programaId);
                conexao.Open();
                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    todosProfessores.Add(new Professor
                    {
                        ProfessorId = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Email = reader.GetString(2),
                        Titulo = reader.GetString(3),
                        Ativo = reader.GetBoolean(4)
                    });
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar todos os professores do programa: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return todosProfessores;
        }
    }
}

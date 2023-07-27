using ListaPresencaAPP.Models;
using ListaPresencaAPP.Services;
using System.Data.SqlClient;
using System;

namespace ListaPresencaAPP.Repositories
{
    public class ProfessorRepository
    {
        private readonly SqlConnection conexao;

        public ProfessorRepository()
        {
            conexao = new SqlConnection(Database.ObterConexao);
        }

        public void AdicionarProfessor(Professor professor)
        {
            string query = "INSERT INTO Professores (Nome, Email, Titulo, Ativo) VALUES (@Nome, @Email, @Titulo, @Ativo)";

            SqlCommand command = new(query, conexao);
            command.Parameters.AddWithValue("@Nome", professor.Nome);
            command.Parameters.AddWithValue("@Email", professor.Email);
            command.Parameters.AddWithValue("@Titulo", professor.Titulo);
            command.Parameters.AddWithValue("@Ativo", professor.Ativo);

            conexao.Open();
            command.ExecuteNonQuery();
            conexao.Close();
        }

        public Professor? ObterProfessorPorId(int id)
        {
            string query = "SELECT Id, Nome, Email, Titulo, Ativo FROM Professores WHERE Id = @Id";

            SqlCommand command = new(query, conexao);
            command.Parameters.AddWithValue("@Id", id);

            conexao.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Professor
                    {
                        Id = (int)reader["Id"],
                        Nome = (string)reader["Nome"],
                        Email = (string)reader["Email"],
                        Titulo = (string)reader["Titulo"],
                        Ativo = (bool)reader["Ativo"]
                    };
                }
            }

            conexao.Close();
            return null;
        }

        public List<Professor> ObterProfessoresAtivos()
        {
            string query = "SELECT Id, Nome, Email, Titulo, Ativo FROM Professores WHERE Ativo = 1";

            SqlCommand command = new(query, conexao);
            conexao.Open();

            List<Professor> professores = new();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    professores.Add(new Professor
                    {
                        Id = (int)reader["Id"],
                        Nome = (string)reader["Nome"],
                        Email = (string)reader["Email"],
                        Titulo = (string)reader["Titulo"],
                        Ativo = (bool)reader["Ativo"]
                    });
                }
            }

            conexao.Close();
            return professores;
        }

        public void AtualizarProfessor(Professor professorAtualizado)
        {
            string query = "UPDATE Professores SET Nome = @Nome, Email = @Email, Titulo = @Titulo, Ativo = @Ativo WHERE Id = @Id";

            SqlCommand command = new(query, conexao);
            command.Parameters.AddWithValue("@Id", professorAtualizado.Id);
            command.Parameters.AddWithValue("@Nome", professorAtualizado.Nome);
            command.Parameters.AddWithValue("@Email", professorAtualizado.Email);
            command.Parameters.AddWithValue("@Titulo", professorAtualizado.Titulo);
            command.Parameters.AddWithValue("@Ativo", professorAtualizado.Ativo);

            conexao.Open();
            command.ExecuteNonQuery();
            conexao.Close();
        }

        public void RemoverProfessor(int id)
        {
            string query = "UPDATE Professores SET Ativo = 0 WHERE Id = @Id";

            SqlCommand command = new(query, conexao);
            command.Parameters.AddWithValue("@Id", id);

            conexao.Open();
            command.ExecuteNonQuery();
            conexao.Close();
        }
    }

}

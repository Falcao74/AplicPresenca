using ListaPresencaAPP.Models;
using ListaPresencaAPP.Services;
using System.Data.SqlClient;


namespace ListaPresencaAPP.Repositories
{

    public class AlunoRepository
    {
        private readonly SqlConnection conexao;

        public AlunoRepository()
        {
            conexao = new SqlConnection(Database.ObterConexao);
        }

        public void AdicionarAluno(Aluno aluno)
        {
            string query = "INSERT INTO Alunos (Matricula, Nome, Email, DataNascimento, Ativo) VALUES (@Matricula, @Nome, @Email, @DataNascimento, @Ativo)";

            SqlCommand command = new SqlCommand(query, conexao);
            command.Parameters.AddWithValue("@Matricula", aluno.Matricula);
            command.Parameters.AddWithValue("@Nome", aluno.Nome);
            command.Parameters.AddWithValue("@Email", aluno.Email);
            command.Parameters.AddWithValue("@DataNascimento", aluno.DataNascimento);
            command.Parameters.AddWithValue("@Ativo", aluno.Ativo);

            conexao.Open();
            command.ExecuteNonQuery();
            conexao.Close();
        }

        public Aluno? ObterAlunoPorMatricula(int matricula)
        {
            string query = "SELECT Matricula, Nome, Email, DataNascimento, Ativo FROM Alunos WHERE Matricula = @Matricula";

            SqlCommand command = new SqlCommand(query, conexao);
            command.Parameters.AddWithValue("@Matricula", matricula);

            conexao.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Aluno
                    {
                        Matricula = (int)reader["Matricula"],
                        Nome = (string)reader["Nome"],
                        Email = (string)reader["Email"],
                        DataNascimento = (DateTime)reader["DataNascimento"],
                        Ativo = (bool)reader["Ativo"]
                    };
                }
            }

            conexao.Close();
            return null;
        }

        public List<Aluno> ObterAlunosAtivos()
        {
            string query = "SELECT Matricula, Nome, Email, DataNascimento, Ativo FROM Alunos WHERE Ativo = 1";

            SqlCommand command = new SqlCommand(query, conexao);
            conexao.Open();

            List<Aluno> alunos = new List<Aluno>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    alunos.Add(new Aluno
                    {
                        Matricula = (int)reader["Matricula"],
                        Nome = (string)reader["Nome"],
                        Email = (string)reader["Email"],
                        DataNascimento = (DateTime)reader["DataNascimento"],
                        Ativo = (bool)reader["Ativo"]
                    });
                }
            }

            conexao.Close();
            return alunos;
        }

        public void AtualizarAluno(Aluno alunoAtualizado)
        {
            string query = "UPDATE Alunos SET Nome = @Nome, Email = @Email, DataNascimento = @DataNascimento, Ativo = @Ativo WHERE Matricula = @Matricula";

            SqlCommand command = new SqlCommand(query, conexao);
            command.Parameters.AddWithValue("@Matricula", alunoAtualizado.Matricula);
            command.Parameters.AddWithValue("@Nome", alunoAtualizado.Nome);
            command.Parameters.AddWithValue("@Email", alunoAtualizado.Email);
            command.Parameters.AddWithValue("@DataNascimento", alunoAtualizado.DataNascimento);
            command.Parameters.AddWithValue("@Ativo", alunoAtualizado.Ativo);

            conexao.Open();
            command.ExecuteNonQuery();
            conexao.Close();
        }

        public void RemoverAluno(int matricula)
        {
            string query = "UPDATE Alunos SET Ativo = 0 WHERE Matricula = @Matricula";

            SqlCommand command = new SqlCommand(query, conexao);
            command.Parameters.AddWithValue("@Matricula", matricula);

            conexao.Open();
            command.ExecuteNonQuery();
            conexao.Close();
        }
    }

}

using ListaPresencaAPP.Models;
using ListaPresencaAPP.Services;
using System.Data.SqlClient;

namespace ListaPresencaAPP.Repositories
{
    public class AulaRepository
    {
        private readonly SqlConnection conexao;

        public AulaRepository()
        {
            conexao = new SqlConnection(Database.ObterConexao);
        }

        public void AdicionarAula(Aula aula)
        {
            string query = "INSERT INTO Aulas (Disciplina, Data, Horario, Ativo) VALUES (@Disciplina, @Data, @Horario, @Ativo)";

            SqlCommand command = new(query, conexao);
            command.Parameters.AddWithValue("@Disciplina", aula.Disciplina);
            command.Parameters.AddWithValue("@Data", aula.Data);
            command.Parameters.AddWithValue("@Horario", aula.Horario);
            command.Parameters.AddWithValue("@Ativo", aula.Ativo);

            conexao.Open();
            command.ExecuteNonQuery();
            conexao.Close();
        }

        public Aula? ObterAulaPorId(int id)
        {
            string query = "SELECT Id, Disciplina, Data, Horario, Ativo FROM Aulas WHERE Id = @Id";

            SqlCommand command = new(query, conexao);
            command.Parameters.AddWithValue("@Id", id);

            conexao.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Aula
                    {
                        Id = (int)reader["Id"],
                        Disciplina = (string)reader["Disciplina"],
                        Data = (DateTime)reader["Data"],
                        Horario = (string)reader["Horario"],
                        Ativo = (bool)reader["Ativo"]
                    };
                }
            }

            conexao.Close();
            return null;
        }

        public List<Aula> ObterAulasAtivas()
        {
            string query = "SELECT Id, Disciplina, Data, Horario, Ativo FROM Aulas WHERE Ativo = 1";

            SqlCommand command = new(query, conexao);
            conexao.Open();

            List<Aula> aulas = new();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    aulas.Add(new Aula
                    {
                        Id = (int)reader["Id"],
                        Disciplina = (string)reader["Disciplina"],
                        Data = (DateTime)reader["Data"],
                        Horario = (string)reader["Horario"],
                        Ativo = (bool)reader["Ativo"]
                    });
                }
            }

            conexao.Close();
            return aulas;
        }

        public void AtualizarAula(Aula aulaAtualizada)
        {
            string query = "UPDATE Aulas SET Disciplina = @Disciplina, Data = @Data, Horario = @Horario, Ativo = @Ativo WHERE Id = @Id";

            SqlCommand command = new(query, conexao);
            command.Parameters.AddWithValue("@Id", aulaAtualizada.Id);
            command.Parameters.AddWithValue("@Disciplina", aulaAtualizada.Disciplina);
            command.Parameters.AddWithValue("@Data", aulaAtualizada.Data);
            command.Parameters.AddWithValue("@Horario", aulaAtualizada.Horario);
            command.Parameters.AddWithValue("@Ativo", aulaAtualizada.Ativo);

            conexao.Open();
            command.ExecuteNonQuery();
            conexao.Close();
        }

        public void RemoverAula(int id)
        {
            string query = "UPDATE Aulas SET Ativo = 0 WHERE Id = @Id";

            SqlCommand command = new(query, conexao);
            command.Parameters.AddWithValue("@Id", id);

            conexao.Open();
            command.ExecuteNonQuery();
            conexao.Close();
        }
    }

}

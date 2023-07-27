using ListaPresencaAPP.Models;
using ListaPresencaAPP.Services;
using System.Data.SqlClient;

namespace ListaPresencaAPP.Repositories
{

    public class ListaPresencaRepository
    {
        private readonly SqlConnection conexao;

        public ListaPresencaRepository()
        {
            conexao = new SqlConnection(Database.ObterConexao);
        }

        public void AdicionarListaPresenca(ListaPresenca listaPresenca)
        {
            string query = "INSERT INTO ListaPresenca (DataCriacao, IdAula, IdProfessor) VALUES (@DataCriacao, @IdAula, @IdProfessor)";

            SqlCommand command = new(query, conexao);
            command.Parameters.AddWithValue("@DataCriacao", listaPresenca.DataCriacao);
            command.Parameters.AddWithValue("@IdAula", listaPresenca.IdAula);
            command.Parameters.AddWithValue("@IdProfessor", listaPresenca.IdProfessor);
            // Converter a lista de IDs dos alunos presentes para uma string separada por vírgulas
            string idsAlunosPresentes = string.Join(",", listaPresenca.IdsAlunosPresentes);
            command.Parameters.AddWithValue("@IdsAlunosPresentes", idsAlunosPresentes);

            conexao.Open();
            command.ExecuteNonQuery();
            conexao.Close();
        }

        public ListaPresenca? ObterListaPresencaPorId(int id)
        {
            string query = "SELECT DataCriacao, IdAula, IdProfessor, IdsAlunosPresentes FROM ListaPresenca WHERE Id = @Id";

            SqlCommand command = new(query, conexao);
            command.Parameters.AddWithValue("@Id", id);

            conexao.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    ListaPresenca listaPresenca = new ListaPresenca
                    {
                        Id = id,
                        DataCriacao = (DateTime)reader["DataCriacao"],
                        IdAula = (int)reader["IdAula"],
                        IdProfessor = (int)reader["IdProfessor"]
                    };

                    // Converter a string dos IDs dos alunos presentes para uma lista de IDs de alunos
                    string idsAlunosPresentesString = (string)reader["IdsAlunosPresentes"];
                    List<int> idsAlunosPresentes = new List<int>(Array.ConvertAll(idsAlunosPresentesString.Split(','), int.Parse));

                    listaPresenca.IdsAlunosPresentes = idsAlunosPresentes;
                    return listaPresenca;
                }
            }

            conexao.Close();
            return null;
        }

        // Implemente outros métodos conforme necessário para manipular os dados da lista de presença no banco de dados.
    }

}

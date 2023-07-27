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
    public class AulaController : EntidadeBase<Aula>
    {
        public override void Adicionar(Aula aula)
        {
            string query = "INSERT INTO Aula (Disciplina, Data, Horario, Ativo) " +
                           "VALUES (@Disciplina, @Data, @Horario, @Ativo);";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@Disciplina", aula.Disciplina);
                comando.Parameters.AddWithValue("@Data", aula.Data);
                comando.Parameters.AddWithValue("@Horario", aula.Horario);
                comando.Parameters.AddWithValue("@Ativo", aula.Ativo);
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao adicionar a aula: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public override void Atualizar(Aula entidade)
        {
            string query = "UPDATE Aula " +
                        "SET Disciplina = @Disciplina, " +
                        "    Data = @Data, " +
                        "    Horario = @Horario, " +
                        "    Ativo = @Ativo " +
                        "WHERE Id = @Id;";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@Id", entidade.Id);
                comando.Parameters.AddWithValue("@Disciplina", entidade.Disciplina);
                comando.Parameters.AddWithValue("@Data", entidade.Data);
                comando.Parameters.AddWithValue("@Horario", entidade.Horario);
                comando.Parameters.AddWithValue("@Ativo", entidade.Ativo);
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao atualizar a aula: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public override void Deletar(int id)
        {
            string query = "UPDATE Aula SET Ativo = 0 WHERE Id = @Id;";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@Id", id);
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao desativar a aula: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public override Aula ObterPorId(int id)
        {
            string query = "SELECT Id, Disciplina, Data, Horario, Ativo " +
                       "FROM Aula " +
                       "WHERE Id = @Id;";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@Id", id);
                conexao.Open();
                using SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    Aula aula = new()
                    {
                        Id = reader.GetInt32(0),
                        Disciplina = reader.GetString(1),
                        Data = reader.GetDateTime(2),
                        Horario = reader.GetString(3),
                        Ativo = reader.GetBoolean(4)
                    };
                    return aula;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao obter a aula por Id: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return new Aula();
        }
        public List<Aula> ListarAulasAtivas(int? programaId = null)
        {
            List<Aula> aulasAtivas = new();
            string query = "SELECT Id, Disciplina, Data, Horario, Ativo FROM Aula WHERE Ativo = 1";

            if (programaId.HasValue)
            {
                query += " AND Id IN (SELECT IdAula FROM ProgramaAula WHERE ProgramaId = @ProgramaId)";
            }

            try
            {
                using SqlCommand comando = new(query, conexao);
                if (programaId.HasValue)
                {
                    comando.Parameters.AddWithValue("@ProgramaId", programaId.Value);
                }
                conexao.Open();

                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    aulasAtivas.Add(new Aula
                    {
                        Id = reader.GetInt32(0),
                        Disciplina = reader.GetString(1),
                        Data = reader.GetDateTime(2),
                        Horario = reader.GetString(3),
                        Ativo = reader.GetBoolean(4)
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar as aulas ativas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return aulasAtivas;
        }
        public List<Aula> ListarAulasInativas(int? programaId = null)
        {
            List<Aula> aulasInativas = new();
            string query = "SELECT Id, Disciplina, Data, Horario, Ativo FROM Aula WHERE Ativo = 0";

            if (programaId.HasValue)
            {
                query += " AND Id IN (SELECT IdAula FROM ProgramaAula WHERE ProgramaId = @ProgramaId)";
            }

            try
            {
                using SqlCommand comando = new(query, conexao);
                if (programaId.HasValue)
                {
                    comando.Parameters.AddWithValue("@ProgramaId", programaId.Value);
                }
                conexao.Open();

                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    aulasInativas.Add(new Aula
                    {
                        Id = reader.GetInt32(0),
                        Disciplina = reader.GetString(1),
                        Data = reader.GetDateTime(2),
                        Horario = reader.GetString(3),
                        Ativo = reader.GetBoolean(4)
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar as aulas inativas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return aulasInativas;
        }
        public List<Aula> ListarTodasAulas(int? programaId = null)
        {
            List<Aula> todasAulas = new();
            string query = "SELECT Id, Disciplina, Data, Horario, Ativo FROM Aula";

            if (programaId.HasValue)
            {
                query += " WHERE Id IN (SELECT IdAula FROM ProgramaAula WHERE ProgramaId = @ProgramaId)";
            }

            try
            {
                using SqlCommand comando = new(query, conexao);
                if (programaId.HasValue)
                {
                    comando.Parameters.AddWithValue("@ProgramaId", programaId.Value);
                }
                conexao.Open();

                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    todasAulas.Add(new Aula
                    {
                        Id = reader.GetInt32(0),
                        Disciplina = reader.GetString(1),
                        Data = reader.GetDateTime(2),
                        Horario = reader.GetString(3),
                        Ativo = reader.GetBoolean(4)
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar todas as aulas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return todasAulas;
        }
    }

}


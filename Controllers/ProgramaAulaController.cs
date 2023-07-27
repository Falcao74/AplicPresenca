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
    public class ProgramaAulaController : EntidadeBase<ProgramaAula>
    {
        public override void Adicionar(ProgramaAula entidade)
        {
            string query = "INSERT INTO ProgramaAula (ProgramaId, AulaId, Ativo) VALUES (@ProgramaId, @AulaId, @Ativo);";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@ProgramaId", entidade.ProgramaId);
                comando.Parameters.AddWithValue("@AulaId", entidade.AulaId);
                comando.Parameters.AddWithValue("@Ativo", entidade.Ativo);
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao associar a aula ao programa: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void Atualizar(ProgramaAula entidade)
        {
            string query = "UPDATE ProgramaAula SET Ativo = @Ativo WHERE ProgramaId = @ProgramaId AND AulaId = @AulaId;";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@ProgramaId", entidade.ProgramaId);
                comando.Parameters.AddWithValue("@AulaId", entidade.AulaId);
                comando.Parameters.AddWithValue("@Ativo", entidade.Ativo);
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao atualizar a associação da aula com o programa: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public override void Deletar(int id)
        {
            throw new NotImplementedException();
        }
        public void Desativar(int programaId, int aulaId)
        {
            string query = "UPDATE ProgramaAula SET Ativo = 0 WHERE ProgramaId = @ProgramaId AND AulaId = @AulaId;";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@ProgramaId", programaId);
                comando.Parameters.AddWithValue("@AulaId", aulaId);
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao desativar a associação da aula com o programa: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override ProgramaAula ObterPorId(int id)
        {
            throw new NotImplementedException();
            
        }
        public Aula ObterAulaPorIdDoPrograma(int programaId, int aulaId)
        {
            string query = "SELECT a.Id, a.Disciplina, a.Data, a.Horario, a.Ativo " +
                           "FROM Aula a " +
                           "INNER JOIN ProgramaAula pa ON a.Id = pa.AulaId " +
                           "WHERE pa.ProgramaId = @ProgramaId AND a.Id = @AulaId AND a.Ativo = 1;";

            
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@ProgramaId", programaId);
                comando.Parameters.AddWithValue("@AulaId", aulaId);
                conexao.Open();

                using SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    return (new Aula
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
                MessageBox.Show("Ocorreu um erro ao obter a aula por ID do programa: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return new Aula();
        }
    }
    }


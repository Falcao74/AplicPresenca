using ListaPresencaAPP.Helpers;
using ListaPresencaAPP.Models;
using System.Data.SqlClient;

namespace ListaPresencaAPP.Controllers
{
    public class AlunoController : EntidadeBase<Aluno>
    {
        public override void Adicionar(Aluno entidade)
        {
            string query = "INSERT INTO ALUNO (Nome, Email, DataNascimento, Ativo) VALUES (@Nome, @Email, @Nascimento, @Ativo); ";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@Nome", entidade.Nome);
                comando.Parameters.AddWithValue("@Email", entidade.Email);
                comando.Parameters.AddWithValue("@Nascimento", entidade.DataNascimento);
                comando.Parameters.AddWithValue("@Ativo", entidade.Ativo);
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao adicionar o aluno: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public override void Atualizar(Aluno entidade)
        {
            string query = "UPDATE ALUNO SET Nome = @Nome, Email = @Email, DataNascimento = @Nascimento, Ativo = @Ativo WHERE Matricula = @Matricula; ";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@Matricula", entidade.Matricula); // Supondo que Matricula seja a chave primária.
                comando.Parameters.AddWithValue("@Nome", entidade.Nome);
                comando.Parameters.AddWithValue("@Email", entidade.Email);
                comando.Parameters.AddWithValue("@Nascimento", entidade.DataNascimento);
                comando.Parameters.AddWithValue("@Ativo", entidade.Ativo);
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao atualizar o aluno: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public override void Deletar(int id)
        {
            string query = "UPDATE ALUNO SET Ativo = 0 WHERE Matricula = @Matricula; ";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@Matricula", id); // Supondo que Matricula seja a chave primária.
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao deletar o aluno: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public override Aluno ObterPorId(int id)
        {
            string query = "SELECT * FROM ALUNO WHERE Matricula = @Matricula; ";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@Matricula", id); // Supondo que Matricula seja a chave primária.
                conexao.Open();

                using SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    // Ler os dados do SqlDataReader e criar um novo objeto Aluno com as informações encontradas.
                    Aluno aluno = new()
                    {
                        Matricula = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Email = reader.GetString(2),
                        DataNascimento = reader.GetDateTime(3),
                        Ativo = reader.GetBoolean(4)
                    };
                    return aluno;
                }
                else
                {
                    // Caso nenhum aluno seja encontrado com a Matricula fornecida, retornar null ou lançar uma exceção, dependendo da lógica desejada.
                    return new Aluno();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao obter o aluno por ID: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new Aluno();
            }
        }
        public List<Aluno> ListarAlunosAtivos()
        {
            List<Aluno> alunosAtivos = new();
            string query = "SELECT * FROM ALUNO WHERE Ativo = 1; ";
            try
            {
                using SqlCommand comando = new (query, conexao);
                conexao.Open();

                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    // Ler os dados do SqlDataReader e criar um novo objeto Aluno com as informações encontradas.
                    Aluno aluno = new()
                    {
                        Matricula = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Email = reader.GetString(2),
                        DataNascimento = reader.GetDateTime(3),
                        Ativo = reader.GetBoolean(4)
                    };
                    alunosAtivos.Add(aluno);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar os alunos ativos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return alunosAtivos;
        }
        // Implementação do método para listar todos os alunos (ativos e inativos) no banco de dados.
        public List<Aluno> ListarTodosAlunos()
        {
            List<Aluno> todosAlunos = new();
            string query = "SELECT * Ativo FROM ALUNO; ";
            try
            {
                using SqlCommand comando = new(query, conexao);
                conexao.Open();

                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    // Ler os dados do SqlDataReader e criar um novo objeto Aluno com as informações encontradas.
                    Aluno aluno = new()
                    {
                        Matricula = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Email = reader.GetString(2),
                        DataNascimento = reader.GetDateTime(3),
                        Ativo = reader.GetBoolean(4)
                    };
                    todosAlunos.Add(aluno);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar todos os alunos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return todosAlunos;
        }
        public List<Aluno> ListarAlunosInativos()
        {
            List<Aluno> alunosInativos = new();
            string query = "SELECT * FROM ALUNO WHERE Ativo = 0; ";
            try
            {
                using SqlCommand comando = new(query, conexao);
                conexao.Open();

                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    // Ler os dados do SqlDataReader e criar um novo objeto Aluno com as informações encontradas.
                    Aluno aluno = new()
                    {
                        Matricula = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Email = reader.GetString(2),
                        DataNascimento = reader.GetDateTime(3),
                        Ativo = reader.GetBoolean(4)
                    };
                    alunosInativos.Add(aluno);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar os alunos inativos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return alunosInativos;
        }
        public List<Aluno> ListarAlunosAtivosDoPrograma(int programaId)
        {
            List<Aluno> alunosAtivosDoPrograma = new();
            string query = "SELECT a.Matricula, a.Nome, a.Email, a.DataNascimento, a.Ativo " +
                           "FROM ALUNO a " +
                           "INNER JOIN ProgramaAluno pa ON a.Matricula = pa.AlunoMatricula " +
                           "WHERE a.Ativo = 1 AND pa.ProgramaId = @ProgramaId; ";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@ProgramaId", programaId);
                conexao.Open();

                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Aluno aluno = new()
                    {
                        Matricula = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Email = reader.GetString(2),
                        DataNascimento = reader.GetDateTime(3),
                        Ativo = reader.GetBoolean(4)
                    };
                    alunosAtivosDoPrograma.Add(aluno);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar os alunos ativos do programa: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return alunosAtivosDoPrograma;
        }
        public List<Aluno> ListarAlunosInativosDoPrograma(int programaId)
        {
            List<Aluno> alunosInativosDoPrograma = new();
            string query = "SELECT a.Matricula, a.Nome, a.Email, a.DataNascimento, a.Ativo " +
                           "FROM ALUNO a " +
                           "INNER JOIN ProgramaAluno pa ON a.Matricula = pa.AlunoMatricula " +
                           "WHERE a.Ativo = 0 AND pa.ProgramaId = @ProgramaId; ";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@ProgramaId", programaId);
                conexao.Open();

                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Aluno aluno = new()
                    {
                        Matricula = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Email = reader.GetString(2),
                        DataNascimento = reader.GetDateTime(3),
                        Ativo = reader.GetBoolean(4)
                    };
                    alunosInativosDoPrograma.Add(aluno);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar os alunos ativos do programa: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return alunosInativosDoPrograma;
        }
        public List<Aluno> ListarTodosAlunosDoPrograma(int programaId)
        {
            List<Aluno> todosAlunosDoPrograma = new();
            string query = "SELECT a.Matricula, a.Nome, a.Email, a.DataNascimento, a.Ativo " +
                           "FROM ALUNO a " +
                           "INNER JOIN ProgramaAluno pa ON a.Matricula = pa.AlunoMatricula " +
                           "WHERE pa.ProgramaId = @ProgramaId; ";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@ProgramaId", programaId);
                conexao.Open();

                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Aluno aluno = new()
                    {
                        Matricula = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Email = reader.GetString(2),
                        DataNascimento = reader.GetDateTime(3),
                        Ativo = reader.GetBoolean(4)
                    };
                    todosAlunosDoPrograma.Add(aluno);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar os alunos ativos do programa: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return todosAlunosDoPrograma;
        }
        public List<Aluno> ListarAlunosPorMatriculas(List<int> matriculas)
        {
            List<Aluno> alunosPorMatriculas = new();
            string query = "SELECT Matricula, Nome, Email, DataNascimento, Ativo " +
                           "FROM ALUNO " +
                           "WHERE Matricula IN (@Matriculas);";
            try
            {
                using SqlCommand comando = new(query, conexao);
                comando.Parameters.AddWithValue("@Matriculas", string.Join(",", matriculas));
                conexao.Open();

                using SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    alunosPorMatriculas.Add(new Aluno
                    {
                        Matricula = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Email = reader.GetString(2),
                        DataNascimento = reader.GetDateTime(3),
                        Ativo = reader.GetBoolean(4)
                    });
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar os alunos por matrículas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return alunosPorMatriculas;
        }

    }
}

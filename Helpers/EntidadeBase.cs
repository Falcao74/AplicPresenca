using ListaPresencaAPP.Services;
using System.Data.SqlClient;

namespace ListaPresencaAPP.Helpers
{
    public abstract class EntidadeBase<T> : IDisposable
    {
        private bool disposed = false;
        protected readonly SqlConnection conexao;

        protected EntidadeBase()
        {
            conexao = new SqlConnection(Database.ObterConexao);
        }

        public abstract void Adicionar(T entidade);

        public abstract T ObterPorId(int id);

        public abstract void Atualizar(T entidade);

        public abstract void Deletar(int id);

        // Implementação do método Dispose para liberar os recursos da conexão ao ser liberado da memória.
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Liberar recursos gerenciados aqui, como a conexão com o banco de dados.
                    conexao.Dispose();
                }

                // Liberar recursos não gerenciados aqui (se houver algum).

                disposed = true;
            }
        }

        // Implementação do método Dispose público para liberar os recursos.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Finalizador, se necessário, chama Dispose(false).
        ~EntidadeBase()
        {
            Dispose(false);
        }
    }

}

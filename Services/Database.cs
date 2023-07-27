using System.Configuration;
using System.Data.SqlClient;

namespace ListaPresencaAPP.Services
{
    static class Database
    {
        public static string ObterConexao
        {

            //Pegar os dados da conexão
            get
            {
                string? serverName = ConfigurationManager.AppSettings["serverName"];
                string? databaseName = ConfigurationManager.AppSettings["databaseName"];
                string? username = ConfigurationManager.AppSettings["username"];
                string? password = ConfigurationManager.AppSettings["password"];
                return $"Server={serverName};Database={databaseName};User ID={username};Password={password};";
            }
        }
        public static bool TestarConexao()
        {
            bool _return = true;

            using (SqlConnection connection = new(ObterConexao))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception)
                {
                    _return = false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return _return;//retorna se a conexão der ok ou não!
        }
    }
}

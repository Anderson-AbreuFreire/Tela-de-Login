using Npgsql;

namespace Tela_de_Login.Classes
{
    public static class Conexao
    {

        private static string conexaoString = "Host=localhost;Port=5432;Database=pim;User ID=postgres;Password=belofode";

        public static NpgsqlConnection CriarConexao()
        {
            return new NpgsqlConnection(conexaoString);
        }
    }
}

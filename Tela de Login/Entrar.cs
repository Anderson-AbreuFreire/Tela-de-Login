using Npgsql;
using System;
using System.Data;

namespace Tela_de_Login
{
    public class Entrar
    {
        private readonly string conexaoString = "Host=localhost;Port=5432;Database=pim;User ID=postgres;Password=belofode";

        public (bool sucesso, string nome, string departamento, int idFuncionario, string mensagem) Autenticar(string email, string senha)
        {
            try
            {
                using (var conexao = new NpgsqlConnection(conexaoString))
                {
                    conexao.Open();

                    string query = @"
                        SELECT f.id_funcionario, f.nome, f.email, d.nome AS departamento
                        FROM funcionario f
                        INNER JOIN departamento d ON f.id_departamento = d.id_departamento
                        WHERE f.email = @usuario AND f.senha = @senha";

                    using (var comando = new NpgsqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@usuario", email);
                        comando.Parameters.AddWithValue("@senha", senha);

                        using (var reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int idFuncionario = reader.GetInt32(reader.GetOrdinal("id_funcionario"));
                                string nome = reader.GetString(reader.GetOrdinal("nome"));
                                string departamento = reader.GetString(reader.GetOrdinal("departamento"));

                                return (true, nome, departamento, idFuncionario, "Login realizado com sucesso");
                            }
                            else
                            {
                                return (false, null, null, 0, "Senha ou Email incorretos");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, null, null, 0, "Erro ao conectar ao banco de dados: " + ex.Message);
            }
        }
    }
}

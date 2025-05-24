using System;
using Npgsql;

namespace Tela_de_Login.Classes
{
    public class Autenticador
    {
        public (bool sucesso, Funcionario funcionario, string mensagem) Autenticar(string email, string senha)
        {
            try
            {
                using (var conexao = Conexao.CriarConexao())
                {
                    conexao.Open();

                    string query = @"
                        SELECT f.id_funcionario, f.nome, f.email, f.senha, f.id_departamento
                        FROM funcionario f
                        WHERE f.email = @email AND f.senha = @senha";

                    using (var comando = new NpgsqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@email", email);
                        comando.Parameters.AddWithValue("@senha", senha);

                        using (var reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int idFuncionario = reader.GetInt32(reader.GetOrdinal("id_funcionario"));
                                string nome = reader.GetString(reader.GetOrdinal("nome"));
                                string emailUsuario = reader.GetString(reader.GetOrdinal("email"));
                                string senhaUsuario = reader.GetString(reader.GetOrdinal("senha"));
                                int idDepartamento = reader.GetInt32(reader.GetOrdinal("id_departamento"));

                                var funcionario = new Funcionario(idFuncionario, nome, emailUsuario, senhaUsuario, idDepartamento);

                                return (true, funcionario, "Login realizado com sucesso");
                            }
                            else
                            {
                                return (false, null, "Senha ou Email incorretos");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, null, "Erro ao conectar ao banco de dados: " + ex.Message);
            }
        }
    }
}

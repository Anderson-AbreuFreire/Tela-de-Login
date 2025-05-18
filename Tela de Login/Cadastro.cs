using Npgsql;
using System;

namespace Tela_de_Login
{
    public class Cadastro
    {
        private string conexaoString = "Host=localhost;Port=5432;Database=pim;User ID=postgres;Password=belofode";

        public int ObterIdDepartamento(string departamento)
        {
            switch (departamento.ToLower())
            {
                case "rh": return 1;
                case "produção": return 2;
                case "gerência": return 3;
                default: return 0;
            }
        }

        public bool VerificarEmailExistente(string email)
        {
            using (var conexao = new NpgsqlConnection(conexaoString))
            {
                conexao.Open();
                string sql = "SELECT COUNT(*) FROM funcionario WHERE email = @email";
                using (var cmd = new NpgsqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public int CadastrarFuncionario(string nome, string email, string senha, int idDepartamento)
        {
            using (var conexao = new NpgsqlConnection(conexaoString))
            {
                conexao.Open();
                string sql = @"
                    INSERT INTO funcionario (nome, email, senha, id_departamento)
                    VALUES (@nome, @email, @senha, @id_departamento)
                    RETURNING id_funcionario";

                using (var cmd = new NpgsqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);
                    cmd.Parameters.AddWithValue("@id_departamento", idDepartamento);

                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}

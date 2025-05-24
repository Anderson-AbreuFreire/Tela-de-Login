using System;
using Npgsql;

namespace Tela_de_Login.Classes
{
    public class Cadastro
    {
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
            using (var conexao = Conexao.CriarConexao())
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

        public int CadastrarFuncionario(Funcionario funcionario)
        {
            using (var conexao = Conexao.CriarConexao())
            {
                conexao.Open();
                string sql = @"
                    INSERT INTO funcionario (nome, email, senha, id_departamento)
                    VALUES (@nome, @email, @senha, @id_departamento)
                    RETURNING id_funcionario";

                using (var cmd = new NpgsqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("@email", funcionario.Email);
                    cmd.Parameters.AddWithValue("@senha", funcionario.Senha);
                    cmd.Parameters.AddWithValue("@id_departamento", funcionario.IdDepartamento);

                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}

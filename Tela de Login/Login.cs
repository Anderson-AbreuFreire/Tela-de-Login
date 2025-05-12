using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Tela_de_Login
{
    public partial class Login: Form
    {
        public static int IdFuncionarioLogado; 
        public Login()
        {
            InitializeComponent();
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string senha = txtSenha.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string conexaoString = "Host=localhost;Port=5432;Database=pim;User ID=postgres;Password=belofode";

            try
            {
                using (var conexao = new Npgsql.NpgsqlConnection(conexaoString))
                {
                    conexao.Open();

                    string query = @"
                SELECT f.id_funcionario, f.nome, f.email, d.nome AS departamento
                FROM funcionario f
                INNER JOIN departamento d ON f.id_departamento = d.id_departamento
                WHERE f.email = @usuario AND f.senha = @senha";

                    using (var comando = new Npgsql.NpgsqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@usuario", email);
                        comando.Parameters.AddWithValue("@senha", senha);

                        using (var reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IdFuncionarioLogado = reader.GetInt32(reader.GetOrdinal("id_funcionario"));
                                string nome = reader.GetString(reader.GetOrdinal("nome"));
                                string departamento = reader.GetString(reader.GetOrdinal("departamento"));

                                MessageBox.Show($"Bem-vindo(a), {nome}!\nDepartamento: {departamento}", "Login realizado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                var chamados = new Chamados_Historico();
                                chamados.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Senha ou Email incorretos", "Ops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtEmail.Focus();
                                txtSenha.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar ao banco de dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void textEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            int tecla = (int)e.KeyChar; 

            if(!char.IsLetterOrDigit(e.KeyChar) && tecla != 64 && tecla != 08 && tecla != 46)
            {
                e.Handled = true;
                MessageBox.Show("Digite somente letras e números", 
                                "Ops", MessageBoxButtons.OK, 
                                MessageBoxIcon.Warning);

                txtEmail.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cadastrar = new TelaCadastro();
            cadastrar.Show();

            this.Visible = false;
        }
    }
}

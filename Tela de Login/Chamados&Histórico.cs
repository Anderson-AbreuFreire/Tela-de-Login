using System;
using System.Windows.Forms;
using Npgsql;

namespace Tela_de_Login
{
    public partial class Chamados_Historico : Form
    {
        // String de conexão com PostgreSQL
        private string conexaoString = "Host=localhost;Port=5432;Database=pim;User ID=postgres;Password=belofode";

        public Chamados_Historico()
        {
            InitializeComponent();
        }

        private void BtnFecharCham_Click(object sender, EventArgs e)
        {
            var TelaLogin = new Login();
            TelaLogin.Show();
            this.Close();
        }

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            int idFuncionario = Login.IdFuncionarioLogado;
            string categoria = "Geral";
            string status = "Aberto";
            DateTime dataAbertura = DateTime.Now;

            string titulo = textBox1.Text;
            string descricao = textBox2.Text;

            try
            {
                using (var conexao = new NpgsqlConnection(conexaoString))
                {
                    conexao.Open();

                    string query = @"
                INSERT INTO chamado (data_abertura, status, id_funcionario, categoria, titulo, descricao)
                VALUES (@data_abertura, @status, @id_funcionario, @categoria, @titulo, @descricao)
                RETURNING id_chamado;
            ";

                    using (var comando = new NpgsqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@data_abertura", dataAbertura);
                        comando.Parameters.AddWithValue("@status", status);
                        comando.Parameters.AddWithValue("@id_funcionario", idFuncionario);
                        comando.Parameters.AddWithValue("@categoria", categoria);
                        comando.Parameters.AddWithValue("@titulo", titulo);
                        comando.Parameters.AddWithValue("@descricao", descricao);

                        int idChamado = (int)comando.ExecuteScalar();

                        MessageBox.Show($"Chamado enviado com sucesso!\nID do chamado: {idChamado}",
                                        "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao enviar chamado: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void label2_Click(object sender, EventArgs e)
        {
          
        }
    }
}



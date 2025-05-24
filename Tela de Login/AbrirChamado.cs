using System;
using System.Windows.Forms;
using Tela_de_Login.Classes; 

namespace Tela_de_Login
{
    public partial class Chamados_Historico : Form
    {
        public Chamados_Historico()
        {
            InitializeComponent();
        }

        private void BtnFecharCham_Click(object sender, EventArgs e)
        {
            var telaLogin = new Login();
            telaLogin.Show();
            this.Close();
        }

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            int idFuncionario = Login.IdFuncionarioLogado;
            // Categoria Padrão
            string categoria = "Geral"; 
            string titulo = textBox1.Text.Trim();
            string descricao = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(descricao))
            {
                MessageBox.Show("Por favor, preencha título e descrição.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Chamado chamado = new Chamado();
                int idChamado = chamado.EnviarChamado(idFuncionario, categoria, titulo, descricao);

                MessageBox.Show($"Chamado enviado com sucesso!\nID do chamado: {idChamado}",
                                "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);


                textBox1.Clear();
                textBox2.Clear();
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

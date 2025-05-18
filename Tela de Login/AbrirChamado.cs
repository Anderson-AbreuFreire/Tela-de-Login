using System;
using System.Windows.Forms;

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
            var TelaLogin = new Login();
            TelaLogin.Show();
            this.Close();
        }

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            int idFuncionario = Login.IdFuncionarioLogado;
            string categoria = "Geral"; //categoria padrão 
            string titulo = textBox1.Text;
            string descricao = textBox2.Text;

            try
            {
                Chamado chamado = new Chamado();
                int idChamado = chamado.EnviarChamado(idFuncionario, categoria, titulo, descricao);

                MessageBox.Show($"Chamado enviado com sucesso!\nID do chamado: {idChamado}",
                                "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

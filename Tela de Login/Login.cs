using System;
using System.Windows.Forms;
using Tela_de_Login.Classes;

namespace Tela_de_Login
{
    public partial class Login : Form
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

            Autenticador autenticador = new Autenticador();
            var resultado = autenticador.Autenticar(email, senha);

            if (resultado.sucesso)
            {
                IdFuncionarioLogado = resultado.funcionario.Id;


                string nomeDepartamento = NomeDepartamentoPorId(resultado.funcionario.IdDepartamento);

                MessageBox.Show(
                    $"Bem-vindo(a), {resultado.funcionario.Nome}!\nDepartamento: {nomeDepartamento}",
                    resultado.mensagem,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                var chamados = new Chamados_Historico();
                chamados.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(resultado.mensagem, "Ops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                txtSenha.Text = "";
            }
        }

        private string NomeDepartamentoPorId(int idDepartamento)
        {
            switch (idDepartamento)
            {
                case 1:
                    return "RH";
                case 2:
                    return "Produção";
                case 3:
                    return "Gerência";
                default:
                    return "Desconhecido";
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            int tecla = (int)e.KeyChar;

            if (!char.IsLetterOrDigit(e.KeyChar) && tecla != 64 && tecla != 08 && tecla != 46)
            {
                e.Handled = true;
                MessageBox.Show("Digite somente letras e números", "Ops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cadastrar = new TelaCadastro();
            cadastrar.Show();
            this.Visible = false;
        }

        private void textEmail_TextChanged(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }
    }
}

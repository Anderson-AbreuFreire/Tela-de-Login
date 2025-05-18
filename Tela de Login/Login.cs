using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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

            Entrar login = new Entrar();
            var resultado = login.Autenticar(email, senha);

            if (resultado.sucesso)
            {
                IdFuncionarioLogado = resultado.idFuncionario;
                MessageBox.Show($"Bem-vindo(a), {resultado.nome}!\nDepartamento: {resultado.departamento}",
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



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

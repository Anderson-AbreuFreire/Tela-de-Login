using System;
using System.Windows.Forms;
using Npgsql;

namespace Tela_de_Login
{
    public partial class TelaCadastro : Form
    {
        private string conexaoString = "Host=localhost;Port=5432;Database=pim;User ID=postgres;Password=belofode";

        public TelaCadastro()
        {
            InitializeComponent();
        }

        private void BtnCadastrar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string email = txtEmailCad.Text;
            string senha = txtSenhaCad.Text;

            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("Preencha todos os campos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Selecione pelo menos um Departamento.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string departamentoSelecionado = checkedListBox1.CheckedItems[0].ToString();

            try
            {
                Cadastro cadastro = new Cadastro();
                int idDepartamento = cadastro.ObterIdDepartamento(departamentoSelecionado);

                if (idDepartamento == 0)
                {
                    MessageBox.Show("Departamento inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cadastro.VerificarEmailExistente(email))
                {
                    MessageBox.Show("Este email já está cadastrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int idFuncionario = cadastro.CadastrarFuncionario(nome, email, senha, idDepartamento);
                MessageBox.Show($"Cadastro realizado com sucesso!\nID: {idFuncionario}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private int ObterIdDepartamento(string departamento)
        {
            switch (departamento.ToLower())
            {
                case "rh":
                    return 1;
                case "produção":
                    return 2;
                case "gerência":
                    return 3;
                default:
                    return 0;
            }
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtEmailCad.Clear();
            txtSenhaCad.Clear();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            txtNome.Focus();
        }

        private void txtEmailCad_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            int tecla = (int)e.KeyChar;

            if (!char.IsLetterOrDigit(e.KeyChar) && tecla != 64 && tecla != 08 && tecla != 46)
            {
                e.Handled = true;
                MessageBox.Show("Digite somente letras e números",
                                "Ops", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtEmailCad.Focus();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnCancelarCadastro_Click(object sender, EventArgs e)
        {
            var TelaLogin = new Login();
            TelaLogin.Show();
            this.Close();
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void TelaCadastro_Load(object sender, EventArgs e)
        {

        }
    }
}

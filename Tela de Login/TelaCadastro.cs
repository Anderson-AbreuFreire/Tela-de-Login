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

            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("O campo Email é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmailCad.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("O campo Senha é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenhaCad.Focus();
                return;
            }
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Selecione pelo menos um Departamento.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                checkedListBox1.Focus();
                return;
            }

            string departamentoSelecionado = checkedListBox1.CheckedItems[0].ToString();
            int idDepartamento = ObterIdDepartamento(departamentoSelecionado);

            if (idDepartamento == 0)
            {
                MessageBox.Show("Departamento selecionado é inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var conexao = new NpgsqlConnection(conexaoString))
                {
                    conexao.Open();

                    // Verificar se o email já está cadastrado
                    string verificaEmail = "SELECT COUNT(*) FROM funcionario WHERE email = @email";
                    using (var comandoVerifica = new NpgsqlCommand(verificaEmail, conexao))
                    {
                        comandoVerifica.Parameters.AddWithValue("@email", email);
                        int count = Convert.ToInt32(comandoVerifica.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Este email já está cadastrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Inserir novo funcionário
                    string query = @"
                        INSERT INTO funcionario (nome, email, senha, id_departamento)
                        VALUES (@nome, @email, @senha, @id_departamento)
                        RETURNING id_funcionario";

                    using (var comando = new NpgsqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", nome);
                        comando.Parameters.AddWithValue("@email", email);
                        comando.Parameters.AddWithValue("@senha", senha);
                        comando.Parameters.AddWithValue("@id_departamento", idDepartamento);

                        int idFuncionario = (int)comando.ExecuteScalar();

                        MessageBox.Show($"Cadastro realizado com sucesso!\nID do funcionário: {idFuncionario}",
                                        "Sucesso",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                        LimparCampos();
                    }
                }
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

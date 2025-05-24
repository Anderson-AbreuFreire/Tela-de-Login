namespace Tela_de_Login.Classes
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int IdDepartamento { get; set; }

        public Funcionario(int id, string nome, string email, string senha, int idDepartamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
            IdDepartamento = idDepartamento;
        }
    }
}


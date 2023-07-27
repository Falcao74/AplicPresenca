namespace ListaPresencaAPP.Models
{
    public class Aluno
    {
        // Propriedades da classe Aluno
        public int Matricula { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; } // Indica se o aluno está ativo (true) ou inativo (false)

        public Aluno() { }
        public Aluno(int matricula, string nome, string email, DateTime dataNascimento, bool ativo)
        {
            Matricula = matricula;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Ativo = ativo;
        }   
    }
}

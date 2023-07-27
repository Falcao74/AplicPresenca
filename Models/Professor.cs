using System;

namespace ListaPresencaAPP.Models
{
    public class Professor
    {
        // Propriedades da classe Professor
        public int ProfessorId { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Titulo { get; set; }
        public bool Ativo { get; set; } // Indica se o professor está ativo (true) ou inativo (false)

        public Professor(){}
        // Construtor da classe Professor
        public Professor(int professorId, string nome, string email, string titulo, bool ativo)
        {
            ProfessorId = professorId;
            Nome = nome;
            Email = email;
            Titulo = titulo;
            Ativo = ativo;
        }
    }
}

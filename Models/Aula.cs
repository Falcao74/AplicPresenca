namespace ListaPresencaAPP.Models
{

    public class Aula
    {
        // Propriedades da classe Aula
        public int Id { get; set; }
        public string Disciplina { get; set; }
        public DateTime Data { get; set; }
        public string Horario { get; set; }
        public bool Ativo { get; set; } // Indica se a aula está ativa (true) ou inativa (false)

        public Aula() { }
        // Construtor da classe Aula
        public Aula(int id, string disciplina, DateTime data, string horario, bool ativo)
        {
            Id = id;
            Disciplina = disciplina;
            Data = data;
            Horario = horario;
            Ativo = ativo;
        }

    }

}

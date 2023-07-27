namespace ListaPresencaAPP.Models
{
    public class ListaPresenca
    {
        // Propriedades do modelo ListaPresenca
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public int IdAula { get; set; }
        public int IdProfessor { get; set; }
        public List<int> IdsAlunosPresentes { get; set; }

        // Construtor padrão
        public ListaPresenca()
        {
            IdsAlunosPresentes = new List<int>();
        }
    }

}

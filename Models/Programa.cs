using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaPresencaAPP.Models
{
    public class Programa
    {
        public int ProgramaId { get; set; }
        public string? Nome { get; set; }
        public string? Sigla { get; set; }
        public List<int>? AlunosMatriculados { get; set; }
        public List<int>? ProfessoresAtuantes { get; set; }
        // Outras propriedades e relacionamentos específicos do Programa

        public Programa()
        {
            AlunosMatriculados = new List<int>();
            ProfessoresAtuantes = new List<int>();
        }

        public Programa(int programaId, string nome, string sigla, List<int> alunosMatriculados, List<int> professoresAtuantes)
        {
            ProgramaId = programaId;
            Nome = nome;
            Sigla = sigla;
            AlunosMatriculados = alunosMatriculados;
            ProfessoresAtuantes = professoresAtuantes;
        }
    }
}

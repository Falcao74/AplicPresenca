using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaPresencaAPP.Models
{
    public class ProgramaProfessor
    {
        public int ProgramaId { get; set; }
        public int ProfessorId { get; set; }

        public bool Ativo { get; set; }

        public ProgramaProfessor()
        {
        }

        public ProgramaProfessor(int programaId, int professorId, bool ativo)
        {
            ProgramaId = programaId;
            ProfessorId = professorId;
            Ativo = ativo;
        }
    }
}

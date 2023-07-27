using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaPresencaAPP.Models
{
    public class ProgramaAluno
    {
        public int ProgramaId { get; set; }
        public int AlunoMatricula { get; set; }
        public bool Ativo { get; set; }

        public ProgramaAluno()
        {
        }

        public ProgramaAluno(int programaId, int alunoMatricula, bool ativo)
        {
            ProgramaId = programaId;
            AlunoMatricula = alunoMatricula;
            Ativo = ativo;
        }
    }

}

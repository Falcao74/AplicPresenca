using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaPresencaAPP.Models
{
    public class ProgramaAula
    {
        public int ProgramaId { get; set; }
        public int AulaId { get; set; }
        public bool Ativo { get; set; }

        public ProgramaAula()
        {
        }

        public ProgramaAula(int programaId, int aulaId, bool ativo)
        {
            ProgramaId = programaId;
            AulaId = aulaId;
            Ativo = ativo;
        }
    }

}

using ListaPresencaAPP.Models;
using ListaPresencaAPP.Repositories;
using System;
using System.Collections.Generic;

namespace ListaPresencaAPP.Services
{
    public class ListaPresencaService
    {
        private readonly ProfessorRepository professorRepository;
        private readonly AlunoRepository alunoRepository;
        private readonly AulaRepository aulaRepository;
        private readonly ListaPresencaRepository listaPresencaRepository;

        public ListaPresencaService()
        {
            professorRepository = new ProfessorRepository();
            alunoRepository = new AlunoRepository();
            aulaRepository = new AulaRepository();
            listaPresencaRepository = new ListaPresencaRepository();
        }

        // Método para adicionar um aluno em uma aula específica
        public bool AdicionarAlunoEmAula(int matriculaAluno, int idAula)
        {
            Aluno? aluno = alunoRepository.ObterAlunoPorMatricula(matriculaAluno);
            Aula? aula = aulaRepository.ObterAulaPorId(idAula);

            if (aluno == null || aula == null)
            {
                return false;
            }

            // Implemente a lógica para adicionar o aluno à lista de presença da aula
            ListaPresenca? listaPresenca = listaPresencaRepository.ObterListaPresencaPorId(aula.Id);

            if (listaPresenca == null)
            {
                // Se a lista de presença ainda não foi criada para esta aula, vamos criá-la.
                listaPresenca = new ListaPresenca
                {
                    DataCriacao = DateTime.Now,
                    IdAula = aula.Id,
                    IdProfessor = aula.IdProfessor,
                    IdsAlunosPresentes = new List<int>()
                };
            }

            if (!listaPresenca.IdsAlunosPresentes.Contains(aluno.Matricula))
            {
                listaPresenca.IdsAlunosPresentes.Add(aluno.Matricula);
            }

            listaPresencaRepository.AdicionarListaPresenca(listaPresenca);
            return true;
        }

        // Método para remover um aluno de uma aula específica (remover da lista de presença)
        public bool RemoverAlunoDeAula(int matriculaAluno, int idAula)
        {
            Aluno aluno = alunoRepository.ObterAlunoPorMatricula(matriculaAluno);
            Aula aula = aulaRepository.ObterAulaPorId(idAula);

            if (aluno == null || aula == null)
            {
                return false;
            }

            // Implemente a lógica para remover o aluno da lista de presença da aula
            ListaPresenca listaPresenca = listaPresencaRepository.ObterListaPresencaPorId(aula.Id);

            if (listaPresenca != null && listaPresenca.IdsAlunosPresentes.Contains(aluno.Matricula))
            {
                listaPresenca.IdsAlunosPresentes.Remove(aluno.Matricula);
                listaPresencaRepository.AdicionarListaPresenca(listaPresenca);
            }

            return true;
        }

        // Método para obter a lista de presença de uma aula específica
        public List<Aluno> ObterListaPresenca(int idAula)
        {
            Aula aula = aulaRepository.ObterAulaPorId(idAula);

            if (aula == null)
            {
                return null;
            }

            // Implemente a lógica para obter a lista de presença da aula
            ListaPresenca listaPresenca = listaPresencaRepository.ObterListaPresencaPorId(aula.Id);

            if (listaPresenca == null)
            {
                return new List<Aluno>();
            }

            List<Aluno> alunosPresentes = new List<Aluno>();
            foreach (int matricula in listaPresenca.IdsAlunosPresentes)
            {
                Aluno aluno = alunoRepository.ObterAlunoPorMatricula(matricula);
                if (aluno != null)
                {
                    alunosPresentes.Add(aluno);
                }
            }

            return alunosPresentes;
        }

        // Resto do código da classe...
    }

}

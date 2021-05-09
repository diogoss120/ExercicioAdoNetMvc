using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExercicioAdoNetMvc.Models
{
    public interface IAluno
    {
        List<Aluno> GetAlunos();
        void InserirAluno(Aluno aluno);
        void AtualizarAluno(Aluno aluno);
        void DeletarAluno(int Id);
        Aluno GetAlunoById(int Id);
    }
}

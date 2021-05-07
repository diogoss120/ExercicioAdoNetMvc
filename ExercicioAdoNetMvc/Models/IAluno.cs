using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExercicioAdoNetMvc.Models
{
    public interface IAluno
    {
        List<Aluno> GetAlunos();
    }
}

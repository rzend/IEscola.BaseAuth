using System;
using System.Collections.Generic;

namespace IEscola.Domain.Entities
{
    public class Professor : EntityBase
    {
        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public ICollection<Disciplina> Disciplinas { get; set; }

        public ICollection<Aluno> Alunos { get; set; }
    }
}

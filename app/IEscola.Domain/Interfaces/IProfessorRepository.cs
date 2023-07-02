﻿using IEscola.Domain.Entities;
using System;
using System.Collections.Generic;

namespace IEscola.Domain.Interfaces
{
    public interface IProfessorRepository
    {
        IEnumerable<Professor> Get();
        Professor Get(Guid id);
        void Insert(Professor disciplina);
        void Update(Professor disciplina);
        void Delete(Professor disciplina);
    }
}

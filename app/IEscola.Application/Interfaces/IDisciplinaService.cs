using System;
using System.Collections.Generic;
using IEscola.Application.HttpObjects.Disciplina.Request;
using IEscola.Domain.Entities;
using System.Text;

namespace IEscola.Application.Interfaces
{
    public interface IDisciplinaService
    {
        IEnumerable<DisciplinaResponse> Get();
        DisciplinaResponse Get(Guid id);

        DisciplinaResponse Insert(DisciplinaInsertRequest disciplina);
        DisciplinaResponse Update(DisciplinaUpdateRequest disciplina);
        void Delete(Disciplina disciplina);
    }
}

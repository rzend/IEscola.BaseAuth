using System;
using System.Collections.Generic;
using IEscola.Application.HttpObjects.Disciplina.Request;
using IEscola.Domain.Entities;
using System.Text;

namespace IEscola.Application.Interfaces
{
    public interface IProfessorService
    {
        IEnumerable<ProfessorResponse> Get();
        ProfessorResponse Get(Guid id);

        ProfessorResponse Insert(ProfessorInsertRequest disciplina);
        ProfessorResponse Update(ProfessorUpdateRequest disciplina);
        void Delete(Disciplina disciplina);
    }
}

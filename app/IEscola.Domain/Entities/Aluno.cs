﻿using System;

namespace IEscola.Domain.Entities
{
    public class Aluno : EntityBase
    {
        public Aluno(Guid id, string nome, DateTime dataNascimento, int numeroMatricula)
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            NumeroMatricula = numeroMatricula;
        }

        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public int NumeroMatricula { get; set; }
    }
}

using System;

namespace IEscola.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime DataUltimaAlteracao { get; private set; }
        public string UsuarioCadastro { get; set; }
        public string UsuarioUltimaAlteracao { get; set; }
        public bool Ativo { get; private set; }

        public EntityBase()
        {
            DataCadastro = DateTime.Now;
            DataUltimaAlteracao = DateTime.Now;
            Ativo = true;      
        }
        public void Ativar() => Ativo = true;
        
        public void Inativar() => Ativo = false;       
    }
}
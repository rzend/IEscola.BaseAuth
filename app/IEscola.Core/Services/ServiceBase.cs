using IEscola.Core.Notificacoes;
using IEscola.Core.Notificacoes.Interfaces;

namespace IEscola.Core.Services
{
    public abstract class ServiceBase
    {
        private readonly INotificador _notificador;

        protected ServiceBase(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void NotificarErro(string errorMsg)
        {
            _notificador.Handle(new Notificacao(errorMsg));
        }
    }
}

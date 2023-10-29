using MediatR;
using Tarefas.Domain.Core.Commands;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Domain.Interfaces.Infra.Data;

namespace Tarefas.Domain.Commads
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
            _bus = bus;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }


        public async Task<bool> Commit(bool ignoreNotification = false)
        {
            if ((!ignoreNotification && _notifications.HasNotifications())) return false;
            if (await _uow.Commit()) return true;

            await _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));

            return false;
        }

    }
}

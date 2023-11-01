using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Tarefas.Domain.Commads.ComandModels.Tarefa;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.Tarefa;
using Tarefas.Domain.Models.Tarefa;
using Tarefas.Services.Interfaces.Tarefa;
using Tarefas.Services.ViewModels.Tarefa;

namespace Tarefas.Services.AppServices.Tarefa
{
    public class TarefaAppService : ITarefaAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IHttpContextAccessor _httpContext;
        private readonly DomainNotificationHandler _notifications;
        private readonly ITarefaRepository _repository;

        public TarefaAppService(IMediatorHandler bus, IMapper mapper, INotificationHandler<DomainNotification> notifications, ITarefaRepository repository, IHttpContextAccessor httpContext)
        {
            _bus = bus;
            _mapper = mapper;
            _notifications = (DomainNotificationHandler)notifications;
            _repository = repository;
            _httpContext = httpContext;
        }

        public async Task<TarefaViewModel> Create(TarefaViewModel aceite)
        {
            TarefaCreateCommand command = _mapper.Map<TarefaCreateCommand>(aceite);
            //command.UsuarioRequerenteId = Guid.Parse(_httpContext.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            await _bus.SendCommand(command);

            // Você pode mapear o resultado do comando para um objeto TarefaViewModel aqui, se necessário.

            return aceite; // ou retorne o objeto apropriado, dependendo da lógica do seu aplicativo.
        }

        public async Task Update(TarefaViewModel aceite)
        {
            TarefaEditCommand command = _mapper.Map<TarefaEditCommand>(aceite);
            //command.UsuarioRequerenteId = Guid.Parse(_httpContext.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            await _bus.SendCommand(command);
        }

        public async Task<bool> Delete(Guid id)
        {
            var task = await _repository.GetById(id);
            if (task != null)
            {
                try
                {
                    await DeleteFisicamente(task);
                }
                catch (Exception e)
                {
                    Console.WriteLine("MESSAGE: " + e.Message + "\n INNER: " + e.InnerException);
                    return false;
                }

                return true;
            }

            return false;
        }

        private async Task DeleteFisicamente(TarefaModel task)
        {
            _repository.Delete(task);
            await _repository.SaveChangesAsync();
        }

        public async Task<TarefaViewModel> GetById(Guid id)
        {
            var task = await _repository.GetById(id);
            return _mapper.Map<TarefaViewModel>(task);
        }

        public async Task<IEnumerable<TarefaViewModel>> GetAll()
        {
            var aceites = await _repository.GetAll();
            return _mapper.Map<IEnumerable<TarefaViewModel>>(aceites);
        }

        public async Task<IEnumerable<TarefaViewModel>> GetByFilter(string nome, string responsavel)
        {
            var response = await _repository.GetByFilter(nome, responsavel);
            return _mapper.Map<IEnumerable<TarefaViewModel>>(response);
        }
    }
}

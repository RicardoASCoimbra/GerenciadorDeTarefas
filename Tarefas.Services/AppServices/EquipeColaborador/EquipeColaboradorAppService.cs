using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Tarefas.Domain.Commads.ComandModels.EquipeColaborador;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.EquipeColaborador;
using Tarefas.Domain.Models.EquipeColaborador;
using Tarefas.Services.Interfaces.EquipeColaborador;
using Tarefas.Services.ViewModels.EquipeColaborador;

namespace Tarefas.Services.AppServices.EquipeColaborador
{
    public class EquipeColaboradorAppService : IEquipeColaboradorAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IHttpContextAccessor _httpContext;
        private readonly DomainNotificationHandler _notifications;
        private readonly IEquipeColaboradorRepository _repository;

        public EquipeColaboradorAppService(IMediatorHandler bus, IMapper mapper, INotificationHandler<DomainNotification> notifications,
            IEquipeColaboradorRepository repository, IHttpContextAccessor httpContext)
        {
            _bus = bus;
            _mapper = mapper;
            _notifications = (DomainNotificationHandler)notifications;
            _repository = repository;
            _httpContext = httpContext;
        }

        public async Task Create(EquipeColaboradorViewModel equipe)
        {
            Guid idNoticia = Guid.NewGuid();
            EquipeColaboradorCreateCommand command = _mapper.Map<EquipeColaboradorCreateCommand>(equipe);
            command.UsuarioRequerenteId = Guid.Parse(_httpContext.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            command.Id = idNoticia;
            await _bus.SendCommand(command);
        }

        public async Task Update(EquipeColaboradorViewModel equipe)
        {
            EquipeColaboradorEditCommand command = _mapper.Map<EquipeColaboradorEditCommand>(equipe);
            command.UsuarioRequerenteId = Guid.Parse(_httpContext.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            await _bus.SendCommand(command);
        }

        public async Task<bool> Delete(Guid id)
        {
            var equipe = (await _repository.GetById(id));
            if (equipe != null)
            {

                try
                {
                    await DeleteFisicamente(equipe);
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
        private async Task DeleteFisicamente(EquipeColaboradorModel equipe)
        {
            _repository.Delete(equipe);
            await _repository.SaveChangesAsync();
        }

        public async Task<object> GetById(Guid id)
        {
            var equipe = (await _repository.GetById(id));
            return _mapper.Map<EquipeColaboradorModel>(equipe);
        }

        public async Task<IEnumerable<EquipeColaboradorViewModel>> GetAll()
        {
            var equipes = (await _repository.GetAll());
            return _mapper.Map<IEnumerable<EquipeColaboradorViewModel>>(equipes);
        }

        public async Task<IEnumerable<EquipeColaboradorViewModel>> GetByFilter(string nome, string descricao)
        {
            var response = (await _repository.GetByFilter(nome, descricao));
            return _mapper.ProjectTo<EquipeColaboradorViewModel>(response.AsQueryable());
        }
        public async Task<IEnumerable<EquipeColaboradorViewModel>> GetEquipe(string nome)
        {
            var equipe = (await _repository.GetEquipe(nome));
            return _mapper.Map<IEnumerable<EquipeColaboradorViewModel>>(equipe);
        }


    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Services.Interfaces.Tarefa;
using Tarefas.Services.ViewModels.Tarefa;
using Tarefas.Web.Configurations;
using Tarefas.Web.Configurations.Authorization;

namespace Tarefas.Web.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TarefasController : ApiController
    {
        private readonly ITarefaAppService _appService;

        public TarefasController(ITarefaAppService appService, INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator) : base(notifications, mediator)
        {
            _appService = appService;
        }

        [HttpGet]
        [Route("{id:guid}")]
        //[AllowAnonymous]
        [ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimTodosPerfis)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response();
                }
                return Response(await _appService.GetById(id));
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        //[AllowAnonymous]
        [ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimAdministrativa)]
        public async Task<IActionResult> Put([FromBody] TarefaViewModel postViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response(postViewModel);
                }

                await _appService.Update(postViewModel);

                return Response();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [HttpGet]
        [Route("GetAll")]
        [AllowAnonymous]
        //[ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimTodosPerfis)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response();
                }
                var query = await _appService.GetAll();
                return Response(query);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [HttpPost]
        //[Route("tarefas")]
        // [ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimAdministrativa)]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] TarefaViewModel postViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response(postViewModel);
                }

                await _appService.Create(postViewModel);

                return Response();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [HttpDelete]
        [Route("{id:guid}")]
        [AllowAnonymous]
        ////[ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimTodosPerfis)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response(id);
                }

                await _appService.Delete(id);

                return Response();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("GetByFilter")]
        [AllowAnonymous]
        //[ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimTodosPerfis)]
        public async Task<IActionResult> GetByFilter(string nome, string responsavel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response();
                }
                return Response(await _appService.GetByFilter(nome, responsavel));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Services.Interfaces.EquipeColaborador;
using Tarefas.Services.ViewModels.EquipeColaborador;
using Tarefas.Web.Configurations;
using Tarefas.Web.Configurations.Authorization;

namespace Tarefas.Web.Controllers.EquipeColaborador
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class EquipeColaboradorController : ApiController
    {
        private readonly IEquipeColaboradorAppService _appService;
        public EquipeColaboradorController(
            IEquipeColaboradorAppService appService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
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
        [Route("AlterarEquipe")]
        //[AllowAnonymous]  
        [ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimAdministrativa)]
        public async Task<IActionResult> Put([FromBody] EquipeColaboradorViewModel postViewModel)
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

        [HttpPost]
        [Route("CriarEquipe")]
        //[ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimAdministrativa)]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] EquipeColaboradorViewModel postViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return BadRequest(ModelState);
                }

                await _appService.Create(postViewModel);

                return NoContent(); // Código 204 No Content para uma criação bem-sucedida.
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpDelete("{id:guid}")]
        //[AllowAnonymous]
        [ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimTodosPerfis)]
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
        public async Task<IActionResult> GetByFilter(string nome, string descricao)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response();
                }
                return Response(await _appService.GetByFilter(nome, descricao));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("GetEquipe")]
        [AllowAnonymous]
        //[ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimTodosPerfis)]
        public async Task<IActionResult> GetEquipe(string nome)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response();
                }
                return Response(await _appService.GetEquipe(nome));
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

    }
}

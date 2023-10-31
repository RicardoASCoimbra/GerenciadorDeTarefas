using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Domain.Enuns;
using Tarefas.Services.Interfaces.Usuarios;
using Tarefas.Services.ViewModels.Usuarios;
using Tarefas.Web.Configurations;
using Tarefas.Web.Configurations.Authorization;

namespace Tarefas.Web.Controllers.Usuarios
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioAppService _appService;

        public UsuarioController(
            IUsuarioAppService appService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _appService = appService;
        }

        [HttpGet]
        [ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimTodosPerfis)]
        //[AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = (await _appService.GetAll());
                return Ok(new { items = users });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Ocorreu um erro durante a consulta dos usuários!");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        //[ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimAdministrativa)]
        [Route("CriarUsuario")]
        public async Task<IActionResult> Post([FromBody] UsuarioViewModel postViewModel)
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

        [HttpGet]
        [AllowAnonymous]
        [Route("CheckLogin")]
        //[ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimAdministrativa)]
        public async Task<IActionResult> CheckLogin(string login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response();
                }

                var response = await _appService.GetByLogin(login);
                return response.Count() > 0 ? Response(true) : Response(false);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("CheckExists")]
        //[ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimAdministrativa)]
        public async Task<IActionResult> CheckExists(string login, string cpf)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response();
                }

                return Response(await _appService.CheckExists(login, cpf));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("GetByFilter")]
        //[AllowAnonymous]
        [ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimTodosPerfis)]
        public async Task<IActionResult> GetByFilter(string login, TipoDeAcesso? perfil)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response();
                }
                return Response(await _appService.GetByFilter(login, perfil));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("GetByUser")]
        [AllowAnonymous]
        //[ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimTodosPerfis)]
        public async Task<IActionResult> GetByUser(string cpf)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response();
                }
                return Response(await _appService.GetByUser(cpf));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
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
        //[AllowAnonymous]
        [Route("AlterarUsuario")]
        [ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimAdministrativa)]
        public async Task<IActionResult> Put([FromBody] UsuarioViewModel postViewModel)
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

        [HttpDelete("{id:guid}")]
        //[AllowAnonymous]
        [ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimAdministrativa)]
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
    }
}

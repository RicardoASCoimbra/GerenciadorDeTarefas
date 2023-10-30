using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Services.Interfaces.Login;
using Tarefas.Services.ViewModels.Login;
using Tarefas.Services.ViewModels.Usuarios;
using Tarefas.Web.Configurations;
using Tarefas.Web.Configurations.Authentication;
using Tarefas.Web.Configurations.Authorization;

namespace Tarefas.Web.Controllers.Login
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ApiController
    {
        private readonly ILoginAppService _loginAppService;
        private readonly JwtIssuerOptions _jwtOptions;
        public LoginController(
            ILoginAppService loginAppService,
            JwtIssuerOptions jwtOptions,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _loginAppService = loginAppService;
            _jwtOptions = jwtOptions;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Post([FromBody] LoginViewModel postViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();

                    return Response(postViewModel);

                }
                var user = await _loginAppService.Autenticar(postViewModel);
                return user != null ? Response(GetJwtToken(user)) : Response();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        //[AllowAnonymous]
        [Route("resetarSenha")]
        [ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimAdministrativa)]
        public async Task<IActionResult> Post(string Id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();

                    return Response();

                }
                var user = await _loginAppService.ResetarSenha(Id);
                return Response(user);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("esqueciSenha")]
        //[ClaimRequirement(Util.ClaimNamePermissao, Util.ClaimAdministrativa)]
        public async Task<IActionResult> PostEsquecisenha(string cpf)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();

                    return Response();

                }

                var user = await _loginAppService.EsqueciSenha(cpf);
                return Response(user);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPut]
        [Route("PrimeiroAcesso")]
        [AllowAnonymous]
        public async Task<IActionResult> PrimeiroAcesso([FromBody] PrimeiroAcessoViewModel ViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response(ViewModel);
                }
                var user = await _loginAppService.PrimeiroAcesso(ViewModel);

                return user != null ? Response(GetJwtToken(user)) : Response();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPut]
        [Route("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaViewModel ViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response(ViewModel);
                }
                var user = await _loginAppService.AlterarSenha(ViewModel);

                return Response(user);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        private object GetJwtToken(UsuarioViewModel user)
        {
            TimeSpan validFor = TimeSpan.FromDays(1);
            DateTime expiration = DateTime.Now.Add(validFor);

            string idUsuario = user.Id.ToString();
            string userName = user.Login != null ? user.Login.ToLower() : "";
            string userGivenName = user.Nome ?? "";
            string tipoPerfil = ((int)user.Perfil).ToString();
            string primeiroAcesso = user.PrimeiroAcesso ? "true" : "false";

            var claims = new List<Claim>
            {
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.Id), idUsuario),
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.Login), userName),
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.GivenName), userGivenName),
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.TipoPerfil), tipoPerfil),
                new Claim(Util.GetEnumDescription(ClaimAuthenticatedUser.IsFirstAccess), primeiroAcesso)
            };

            var jwt = new JwtSecurityToken(issuer: _jwtOptions.Issuer, audience: _jwtOptions.Audience,
                                          claims: claims.ToArray(), notBefore: DateTime.Now, expires: expiration,
                                          signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                currentToken = encodedJwt,
                expire_in = (int)validFor.TotalSeconds,
                expire_datetime = expiration
            };

            return response;
        }

    }
}

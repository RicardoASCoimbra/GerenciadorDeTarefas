using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;

namespace Tarefas.Web.Configurations.Authorization
{
    public class ClaimRequirementFilter : IActionFilter
    {
        readonly Claim _claim;

        public ClaimRequirementFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string[] perfis = _claim.Value.Split(';');
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && perfis.Contains(c.Value));
            if (!hasClaim)
                context.Result = new StatusCodeResult((int)HttpStatusCode.Unauthorized);
        }
    }

}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionUI.Admin.CustomFilter
{
    public class UserLoggedFilter : IAsyncPageFilter
    {

        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            //_logger.LogDebug("Global OnPageHandlerSelectionAsync called.");

            bool canAccess = this.CanAccess(context);


            if (!canAccess)
            {
                context.HttpContext.Response.Redirect("/Login");
            }

            await Task.CompletedTask;
        }

        private bool CanAccess(PageHandlerSelectedContext context)
        {
            if (!context.ActionDescriptor.ViewEnginePath.ToLower().Contains("login"))
            {

                var userSession = context.HttpContext.Session.GetString(Constants.USER_SESSION);

                if (string.IsNullOrEmpty(userSession))
                    return false;

            }

            return true;

        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            // _logger.LogDebug("Global OnPageHandlerExecutionAsync called.");
            await next.Invoke();
        }
    }
}

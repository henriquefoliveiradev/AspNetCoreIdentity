﻿using KissLog;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreIdentity.Extensions
{
    public class AuditoriaFilter : IActionFilter
    {
        private readonly IKLogger _logger;

        public AuditoriaFilter(IKLogger logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var message = context.HttpContext.User.Identity.Name + " Acesso: " +
                              context.HttpContext.Request.GetDisplayUrl();

                _logger.Info(message);
            }
        }
    }
}
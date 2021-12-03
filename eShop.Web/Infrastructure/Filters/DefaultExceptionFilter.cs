using eShop.Web.Infrastructure.Errors;

using Microsoft.AspNetCore.Mvc.Filters;

namespace eShop.Web.Infrastructure.Filters
{
    public class DefaultExceptionFilter : ExceptionFilterAttribute
    {
        private readonly WebApiErrorProvider _webApiErrorProvider;
        private readonly ILogger<DefaultExceptionFilter> _logger;

        public DefaultExceptionFilter(WebApiErrorProvider webApiErrorProvider, ILogger<DefaultExceptionFilter> logger)
        {
            _webApiErrorProvider = webApiErrorProvider;
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            context.Result = _webApiErrorProvider.GetError(context.Exception);

            _logger.LogError(new EventId(1, "UnhandledError"), context.Exception,
                "Oops! Something went wrong. Weird things happen. Details: {details}", context.Exception.Message);
        }
    }
}

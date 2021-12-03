using System.Net;

using Microsoft.AspNetCore.Mvc;

namespace eShop.Web.Infrastructure.Errors
{
    public class WebApiErrorProvider
    {
        public IActionResult GetError(Exception exception)
        {
            return InternalServerError(exception);
        }

        private static IActionResult InternalServerError(Exception exception)
        {
            var errorResult = new ErrorResult(HttpStatusCode.InternalServerError.ToString(), exception.Message);

            return new ObjectResult(errorResult)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}

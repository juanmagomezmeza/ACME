using ACME.SchoolManagement.Core.Application.Exceptions;
using ACME.SchoolManagement.Core.Application.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace ACME.SchoolManagement.Api.Filters
{
    /// <summary>
    /// Exceptions filter
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// it is excecuted for each request
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            var exceptionType = context.Exception.GetType();
            string message;

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Unauthorized Access";
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NotImplementedException) || exceptionType == typeof(ArgumentNullException)
                || exceptionType == typeof(InvalidOperationException) || exceptionType == typeof(InvalidCastException))
            {
                message = "A server error occurred.";
                status = HttpStatusCode.InternalServerError;
            }
            else if (exceptionType == typeof(InvalidDataException))
            {
                message = "Invalid data";
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(RequestValidationException))
            {
                var path = GetAbsolutePath(((DefaultHttpContext)context.HttpContext).Request);
                var errors = (context.Exception as RequestValidationException).Errors;
                var errorResponse = new ValidationErrorModel() { Message = $"Request: {path} contains validation Errors", Errors = errors };
                message = JsonConvert.SerializeObject(errorResponse, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
                status = HttpStatusCode.BadRequest;
            }
            else
                message = "ocurrió un error interno";

            context.ExceptionHandled = true;

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            response.WriteAsync(message);
        }

        /// <summary>
        /// Get the absolute path
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetAbsolutePath(HttpRequest request)
        {
            return string.Concat(
                        request.Scheme,
                        "://",
                        request.Host.ToUriComponent(),
                        request.PathBase.ToUriComponent(),
                        request.Path.ToUriComponent(),
                        request.QueryString.ToUriComponent());
        }

    }
}

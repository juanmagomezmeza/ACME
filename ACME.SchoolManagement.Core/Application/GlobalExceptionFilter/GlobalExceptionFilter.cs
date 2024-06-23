using ACME.SchoolManagement.Core.Application.ExceptionHandlers;
using ACME.SchoolManagement.Core.Domain.Contracts.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ACME.SchoolManagement.Api.Filters
{
    /// <summary>
    /// Exceptions filter
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IDictionary<Type, IExceptionHandler> _exceptionHandlers;
        private readonly IExceptionHandler _defaultHandler;

        public GlobalExceptionFilter(IEnumerable<IExceptionHandler> handlers, DefaultExceptionHandler defaultHandler)
        {
            _exceptionHandlers = handlers.ToDictionary(handler => handler.ExceptionType, handler => handler);
            _defaultHandler = defaultHandler ?? throw new ArgumentNullException(nameof(defaultHandler), "Default handler cannot be null");
        }

        public void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            var handler = _exceptionHandlers.ContainsKey(exceptionType) ? _exceptionHandlers[exceptionType] : _defaultHandler;

            var (status, message) = handler.HandleException(context.Exception);

            var response = new { message };
            var responseJson = JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = (int)status;
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.WriteAsync(responseJson).ConfigureAwait(false);
        }
    }




}

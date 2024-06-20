using ACME.SchoolManagement.Core.Domain.Exceptions;
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
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status;
            string message;

            switch (context.Exception)
            {
                case UnauthorizedAccessException _:
                    status = HttpStatusCode.Unauthorized;
                    message = "Unauthorized Access";
                    break;
                case PaymentException ex:
                    status = HttpStatusCode.PaymentRequired; 
                    message = ex.Message;
                    break;
                case DataConsistencyException ex:
                    status = HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;
                case InvalidDataException ex:
                    status = HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;
                default:
                    status = HttpStatusCode.InternalServerError;
                    message = "An internal error occurred.";
                    break;
            }

            var response = new { message };
            var responseJson = JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = (int)status;
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.WriteAsync(responseJson);
        }
    }


}

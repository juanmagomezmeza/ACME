using ACME.SchoolManagement.Core.Application.Contracts;
using ACME.SchoolManagement.Core.Application.Request;
using AutoMapper;

namespace ACME.SchoolManagement.Core.Application.Extensions
{
    internal static class RequestHandlerExtension
    {
        static readonly string ServiceNotFoundMessage = "Service was not found in Service Factory. Register your service implementations with the container/service factory.";

        public static ILoggerService GetLoggerService<TRequest, TResponse>(this IRequestHandler<TRequest, TResponse> handler, ServiceFactory serviceFactory) where TRequest : IRequest<TResponse>
        {
            var logger = serviceFactory.GetInstance<ILoggerService>();
            return logger ?? throw new InvalidOperationException($"Logger {ServiceNotFoundMessage}");
        }

        public static IMapper GetAutomapperService<TRequest, TResponse>(this IRequestHandler<TRequest, TResponse> handler, ServiceFactory serviceFactory) where TRequest : IRequest<TResponse>
        {
            var mapper = serviceFactory.GetInstance<IMapper>();
            return mapper ?? throw new InvalidOperationException($"IMapper {ServiceNotFoundMessage}");
        }

        public static IStudentService GetStudentService<TRequest, TResponse>(this IRequestHandler<TRequest, TResponse> handler, ServiceFactory serviceFactory) where TRequest : IRequest<TResponse>
        {
            var studentservice = serviceFactory.GetInstance<IStudentService>();
            return studentservice ?? throw new InvalidOperationException($"IStudentService {ServiceNotFoundMessage}");
        }

        public static ICourseService GetCourseService<TRequest, TResponse>(this IRequestHandler<TRequest, TResponse> handler, ServiceFactory serviceFactory) where TRequest : IRequest<TResponse>
        {
            var courseService = serviceFactory.GetInstance<ICourseService>();
            return courseService ?? throw new InvalidOperationException($"ICourseService {ServiceNotFoundMessage}");
        }

        public static IEnrollmentService GetEnrollmentService<TRequest, TResponse>(this IRequestHandler<TRequest, TResponse> handler, ServiceFactory serviceFactory) where TRequest : IRequest<TResponse>
        {
            var enrollmentService = serviceFactory.GetInstance<IEnrollmentService>();
            return enrollmentService ?? throw new InvalidOperationException($"IEnrollmentService {ServiceNotFoundMessage}");
        }

        public static IPaymentGateway GetIPaymentGatewayService<TRequest, TResponse>(this IRequestHandler<TRequest, TResponse> handler, ServiceFactory serviceFactory) where TRequest : IRequest<TResponse>
        {
            var paymentGateway = serviceFactory.GetInstance<IPaymentGateway>();
            return paymentGateway ?? throw new InvalidOperationException($"IPaymentGateway {ServiceNotFoundMessage}");
        }
    }
}

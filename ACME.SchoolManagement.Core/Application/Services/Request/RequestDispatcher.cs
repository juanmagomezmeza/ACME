using ACME.SchoolManagement.Core.Application.Extensions;
using ACME.SchoolManagement.Core.Domain.Contracts.Request;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Exceptions;
using System.Collections.Concurrent;
using System.Reflection;

namespace ACME.SchoolManagement.Core.Application.Services.Request
{
    public class RequestDispatcher : IRequestDispatcher
    {
        private readonly ServiceFactory serviceFactory;
        private ILoggerService? logger;
        private static readonly ConcurrentDictionary<Type, Type> requestHandlers = new();

        public RequestDispatcher(ServiceFactory? serviceFactory, bool loadRequestFromAssembly = true)
        {
            this.serviceFactory = serviceFactory ?? throw new NullReferenceException(nameof(ServiceFactory));
            LoadLogService();
            if (loadRequestFromAssembly)
                LoadRequestHandlersFromAssembly();
        }

        private void LoadLogService()
        {
            logger = serviceFactory.GetInstance<ILoggerService>();
        }

        public async Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));

                var handler = GetHandlerByRequest<TRequest, TResponse>(request.GetType(), request.GetType().Name);
                var response = await handler.Handle(request, serviceFactory, cancellationToken);
                return response;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    logger.Error(e.InnerException.Message, e);
                else
                    logger?.Error(e.Message, e);
                throw;
            }
        }

        private IRequestHandler<TRequest, TResponse>? GetHandlerByRequest<TRequest, TResponse>(Type requestType, string requestName) where TRequest : IRequest<TResponse>
        {
            if (!requestHandlers.TryGetValue(requestType, out Type? handlerType))
                throw new RequestHandlerNotFoundException(requestName);

            var handler = serviceFactory.GetInstance(handlerType);
            return handler is null
                ? throw new InvalidOperationException($"Handler was not found for request of type {requestName}. Register your handlers with the container/service factory.")
                : (IRequestHandler<TRequest, TResponse>)handler;
        }

        private static void LoadRequestHandlersFromAssembly()
        {
            if (requestHandlers.Any())
                return;
            Type requestType = typeof(IRequest<>);
            var assembly = Assembly.GetAssembly(requestType) ?? throw new ArgumentNullException("No se encontró el emsamblado para el tipo " + nameof(requestType));

            Type[] allTypes = assembly.GetTypes();
            var requestTypes = requestType.GetImplementedInterfacesFromTypes(allTypes);
            var handlersType = typeof(IRequestHandler<,>);
            var handlers = handlersType.GetImplementedInterfacesFromTypes(allTypes);
            foreach (var req in requestTypes)
            {
                var reqHandler = handlers.Where(h => h.GetInterfaces()[0].GetGenericArguments()[0] == req).FirstOrDefault();
                if (reqHandler != null)
                    requestHandlers.TryAdd(req, reqHandler);
            }
        }

        public void AddRequestHandler<TRequest, TResponse>(IRequestHandler<TRequest, TResponse> handler) where TRequest : IRequest<TResponse>
        {
            if (handler is null)
                throw new ArgumentNullException(nameof(handler));

            var requestType = typeof(TRequest);

            requestHandlers.GetOrAdd(requestType, handler.GetType());
        }

        public bool ContainsRequestHandler<TRequest, TResponse>(IRequestHandler<TRequest, TResponse> handler) where TRequest : IRequest<TResponse>
        {
            if (handler is null)
                throw new ArgumentNullException(nameof(handler));
            var requestType = typeof(TRequest);
            return requestHandlers.ContainsKey(requestType);
        }

    }
}

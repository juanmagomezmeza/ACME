﻿using ACME.SchoolManagement.Core.Application.Extensions;
using ACME.SchoolManagement.Core.Domain.Contracts.Request;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Exceptions;
using System.Collections.Concurrent;
using System.Reflection;

namespace ACME.SchoolManagement.Core.Application.Services.Request
{
    public class RequestDispatcher : IRequestDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILoggerService _logger;
        private static readonly ConcurrentDictionary<Type, Type> requestHandlers = new();

        public RequestDispatcher(IServiceProvider serviceProvider, ILoggerService logger, bool loadRequestFromAssembly = true)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (loadRequestFromAssembly)
                LoadRequestHandlersFromAssembly();
        }

        public async Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));

                var handler = GetHandlerByRequest<TRequest, TResponse>(request.GetType(), request.GetType().Name);
                var response = await handler.Handle(request, cancellationToken);
                return response;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    _logger.Error(e.InnerException.Message, e);
                else
                    _logger.Error(e.Message, e);
                throw;
            }
        }

        private IRequestHandler<TRequest, TResponse>? GetHandlerByRequest<TRequest, TResponse>(Type requestType, string requestName) where TRequest : IRequest<TResponse>
        {
            if (!requestHandlers.TryGetValue(requestType, out Type? handlerType))
                throw new RequestHandlerNotFoundException(requestName);

            var handler = _serviceProvider.GetService(handlerType);
            return handler is null
                ? throw new InvalidOperationException($"Handler was not found for request of type {requestName}. Register your handlers with the container/service provider.")
                : (IRequestHandler<TRequest, TResponse>)handler;
        }

        private static void LoadRequestHandlersFromAssembly()
        {
            if (requestHandlers.Any())
                return;

            Type requestType = typeof(IRequest<>);
            var assembly = Assembly.GetAssembly(requestType) ?? throw new ArgumentNullException("No se encontró el ensamblado para el tipo " + nameof(requestType));

            Type[] allTypes = assembly.GetTypes();
            var requestTypes = requestType.GetImplementedInterfacesFromTypes(allTypes);
            var handlersType = typeof(IRequestHandler<,>);
            var handlers = handlersType.GetImplementedInterfacesFromTypes(allTypes);

            foreach (var req in requestTypes)
            {
                var reqHandler = handlers.FirstOrDefault(h => h.GetInterfaces()[0].GetGenericArguments()[0] == req);
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

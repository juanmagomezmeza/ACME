﻿using ACME.SchoolManagement.Core.Domain.Contracts.Exceptions;
using ACME.SchoolManagement.Core.Domain.Exceptions;
using System.Net;

namespace ACME.SchoolManagement.Core.Application.ExceptionHandlers
{
    public class DataConsistencyExceptionHandler : IExceptionHandler
    {
        public Type ExceptionType => typeof(DataConsistencyException);

        public (HttpStatusCode status, string message) HandleException(Exception exception)
        {
            return (HttpStatusCode.BadRequest, "Data concistency error");
        }
    }
}

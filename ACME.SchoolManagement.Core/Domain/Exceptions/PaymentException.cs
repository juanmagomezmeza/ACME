﻿namespace ACME.SchoolManagement.Core.Domain.Exceptions
{
    public class PaymentException : Exception
    {
        public PaymentException(string message) : base(message) { }
    }
}

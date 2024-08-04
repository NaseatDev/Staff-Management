﻿namespace StaffManagement.Service.Commons.Exceptions
{
    public class BadRequestException : Exception

    {
        public string[] Errors { get; set; }

        public BadRequestException()
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BadRequestException(string[] errors) : base("Multiple errors occurred. See error details.")
        {
            Errors = errors;
        }
    }
}

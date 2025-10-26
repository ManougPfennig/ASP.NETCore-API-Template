using System;

namespace ProjectTemplate.Domain.Exceptions;

public class ConflictException(string message = "This ressource already exists.") : ExceptionBase(message, 409)
{
}

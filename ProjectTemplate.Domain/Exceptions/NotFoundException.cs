using System;

namespace ProjectTemplate.Domain.Exceptions;

public class NotFoundException(string message = "The ressource was not found.") : ExceptionBase(message, 404)
{
}

using System;

namespace ProjectTemplate.Domain.Exceptions;

public abstract class ExceptionBase(string message, int statusCode = 400) : Exception(message)
{
	public int StatusCode { get; } = statusCode;
}

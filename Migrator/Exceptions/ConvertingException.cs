using System;

namespace MigrationTool.Exceptions;

public class ConvertingException : Exception
{
    public ConvertingException(string message) : base(message)
    {
        
    }
}
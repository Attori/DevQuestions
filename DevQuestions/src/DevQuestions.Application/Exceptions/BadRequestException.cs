using System.Text.Json;
using Shared;

namespace DevQuestions.Application.Exceptions;

public class BadRequestException : Exception
{
    protected BadRequestException(params Error[] errors) 
        : base(JsonSerializer.Serialize(errors))
    {
    }
}
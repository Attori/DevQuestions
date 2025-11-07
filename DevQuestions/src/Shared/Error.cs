namespace Shared;

public record Error
{
    private readonly string _code;
    private readonly string _message;
    private readonly ErrorType _type;
    private readonly string? _invalidField;
    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }
    public string? InvalidField { get; }

    private Error(string code, string message, ErrorType type, string? invalidField = null)
    {
        _code = code;
        _message = message;
        _type = type;
        _invalidField = invalidField;
    }

    public static Error NotFound(string? code, string message, Guid? id) =>
        new("record.not.found", message, ErrorType.NOT_FOUND);

    public static Error Validation(string? code, string message, string? invalidField = null) =>
        new(code ?? "value.is.invalid", message, ErrorType.VALIDATION);

    public static Error Conflict(string? code, string message) =>
        new(code ?? "value.is.conflict", message, ErrorType.CONFLICT);

    public static Error Failure(string? code, string message) => new(code ?? "failure", message, ErrorType.FAILURE);
}

public enum ErrorType
{
    /// <summary>
    /// валидация
    /// </summary>
    VALIDATION,

    /// <summary>
    /// не найдено
    /// </summary>
    NOT_FOUND,

    /// <summary>
    /// ошибка
    /// </summary>
    FAILURE,

    /// <summary>
    /// конфликт
    /// </summary>
    CONFLICT
}
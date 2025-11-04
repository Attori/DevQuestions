namespace DevQuestions.Contracts;

public record GetQuestionDto(string Search, Guid[] TagIds, int page, int pageSize);
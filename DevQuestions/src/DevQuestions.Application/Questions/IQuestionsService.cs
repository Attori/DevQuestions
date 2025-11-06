using DevQuestions.Contracts;
using DevQuestions.Domain.Questions;

namespace DevQuestions.Application.Questions;

public interface IQuestionsService
{
    Task<Guid> Create(CreateQuestionDto createQuestionDto, CancellationToken cancellationToken);
}
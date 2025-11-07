using DevQuestions.Application.Extensions;
using DevQuestions.Application.FullTextSearch;
using DevQuestions.Application.Questions.Exceptions;
using DevQuestions.Application.Questions.Fails;
using DevQuestions.Application.Questions.Fails.Exceptions;
using DevQuestions.Contracts;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions;

public class QuestionsService : IQuestionsService
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly ISearchProvider _searchProvider;
    private readonly ILogger<QuestionsService> _logger;
    private readonly IValidator<CreateQuestionDto> _validator;

    public QuestionsService(
        IQuestionsRepository questionsRepository,
        ILogger<QuestionsService> logger,
        IValidator<CreateQuestionDto> validator,
        ISearchProvider searchProvider)
    {
        _questionsRepository = questionsRepository;
        _logger = logger;
        _validator = validator;
        _searchProvider = searchProvider;
    }

    public async Task<Guid> Create(CreateQuestionDto questionDto, CancellationToken cancellationToken)
    {
        // Валидация входных данных
        var validationResult = await _validator.ValidateAsync(questionDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new QuestionValidationException(validationResult.ToErrors());
        }

        // Валидация бизнес логики

        var countOfOpenUserQuestionsCount =
            await _questionsRepository.GetOpenUserQuestionsAsync(questionDto.UserId, cancellationToken);

        var existedQuestion = await _questionsRepository.GetByIdAsync(Guid.Empty, cancellationToken);

        if (countOfOpenUserQuestionsCount > 3)
        {
            throw new ToManyQuestionsException();
        }

        var questionId = Guid.NewGuid();

        var question = new Question(
            questionId,
            questionDto.Title,
            questionDto.Text,
            questionDto.UserId,
            null,
            questionDto.TagIds);

        await _questionsRepository.AddAsync(question, cancellationToken);

        await _searchProvider.IndexQuestionsAsync(question);

        _logger.LogInformation("Created question with id {QuestionId}", questionId);

        return questionId;
    }

    // public async Task<IActionResult> Update([FromRoute] Guid questionId, [FromBody] UpdateQuestionDto request,
    //     CancellationToken cancellationToken)
    // {
    // }
    //
    // public async Task<IActionResult> Delete(Guid questionId, CancellationToken cancellationToken)
    // {
    // }
    //
    // public async Task<IActionResult> SelectSolution(Guid questionId, Guid answerId,
    //     CancellationToken cancellationToken)
    // {
    // }
    //
    // public async Task<IActionResult> AddAnswer(Guid questionId, AddAnswerDto request,
    //     CancellationToken cancellationToken)
    // {
    // }
}
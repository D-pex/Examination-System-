using Examination.Core.Dtos;
using Examination.Core.Requests;
using Examination.Persistence;
using Examination.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

public sealed class QuestionService
{
    private readonly AppDbContext _dbContext;

    public QuestionService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public QuestionDto CreateQuestion(CreateQuestionRequest request)
    {
        if (request.TestId <= 0)
            throw new ConflictException("Invalid TestId");

        if (string.IsNullOrWhiteSpace(request.QuestionText))
            throw new ConflictException("Question text is required");

        var testExists = _dbContext.Tests.Any(t => t.Id == request.TestId);
        if (!testExists)
            throw new ConflictException("Test does not exist");

        if (request.Options == null || request.Options.Count < 2)
            throw new ConflictException("Minimum 2 options required");

        if (request.Options.Count(o => o.IsCorrect) != 1)
            throw new ConflictException("Only one correct option allowed");

        var question = new Question
        {
            TestId = request.TestId,
            QuestionText = request.QuestionText.Trim()
        };

        _dbContext.Questions.Add(question);
        _dbContext.SaveChanges();

        var options = request.Options.Select(o => new Option
        {
            QuestionId = question.Id,
            OptionText = o.OptionText.Trim(),
            IsCorrect = o.IsCorrect
        }).ToList();

        _dbContext.Options.AddRange(options);
        _dbContext.SaveChanges();

        return MapToDto(question, options);
    }

    public List<QuestionDto> GetQuestionsByTestId(int testId)
    {
        if (testId <= 0)
            throw new ConflictException("Invalid TestId");

        var testExists = _dbContext.Tests.Any(t => t.Id == testId);
        if (!testExists)
            throw new ConflictException("Test does not exist");

        var questions = _dbContext.Questions
            .AsNoTracking()
            .Where(q => q.TestId == testId)
            .Include(q => q.Options)
            .ToList();

        return questions.Select(q => MapToDto(q, q.Options)).ToList();
    }

    public void DeleteQuestion(int questionId)
    {
        if (questionId <= 0)
            throw new ConflictException("Invalid QuestionId");

        var question = _dbContext.Questions
            .Include(q => q.Options)
            .FirstOrDefault(q => q.Id == questionId);

        if (question == null)
            throw new ConflictException("Question not found");

        _dbContext.Options.RemoveRange(question.Options);
        _dbContext.Questions.Remove(question);

        _dbContext.SaveChanges();
    }

    private static QuestionDto MapToDto(Question q, List<Option> options)
    {
        return new QuestionDto(
            q.Id,
            q.QuestionText ?? "",
            options.Select(o => new OptionDto(
                o.Id,
                o.OptionText ?? ""
            )).ToList()
        );
    }
}
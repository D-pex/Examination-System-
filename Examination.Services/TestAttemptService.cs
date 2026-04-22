using Examination.Core.Dtos;
using Examination.Core.Requests;
using Examination.Persistence;
using Examination.Services.Exceptions;

namespace Examination.Services;

public sealed class TestAttemptService
{
    private readonly AppDbContext _dbContext;

    public TestAttemptService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public UserAttemptDto StartAttempt(CreateUserAttemptRequest request)
    {
        if (request.UserId <= 0 || request.TestId <= 0)
            throw new ConflictException("Invalid UserId or TestId");

        var userExists = _dbContext.Users.Any(u => u.Id == request.UserId);
        if (!userExists)
            throw new ConflictException("User not found");

        var test = _dbContext.Tests.FirstOrDefault(t => t.Id == request.TestId);
        if (test == null)
            throw new ConflictException("Test not found");

        if (!test.IsPublished)
            throw new ConflictException("Test is not published");

        var attempt = new UserAttempt
        {
            UserId = request.UserId,
            TestId = request.TestId,
            TotalScore = 0,
            IsPassed = false,
            AttemptDate = DateTime.UtcNow
        };

        _dbContext.UserAttempts.Add(attempt);
        _dbContext.SaveChanges();

        return new UserAttemptDto(
            attempt.Id,
            attempt.UserId,
            attempt.TestId,
            attempt.TotalScore,
            attempt.IsPassed,
            attempt.AttemptDate
        );
    }

    public UserAttemptDto SubmitAllAnswers(CreateSubmitAllAnswerRequest request)
    {
        if (request.AttemptId <= 0)
            throw new ConflictException("Invalid AttemptId");

        var attempt = _dbContext.UserAttempts
            .FirstOrDefault(a => a.Id == request.AttemptId);

        if (attempt == null)
            throw new ConflictException("Attempt not found");

        var questions = _dbContext.Questions
            .Where(q => q.TestId == attempt.TestId)
            .ToList();

        foreach (var question in questions)
        {
            var submitted = request.Answers
                .FirstOrDefault(a => a.QuestionId == question.Id);

            int selectedOptionId = submitted?.SelectedOptionId ?? 0;

            if (selectedOptionId > 0)
            {
                var option = _dbContext.Options
                    .FirstOrDefault(o => o.Id == selectedOptionId);

                if (option == null)
                    throw new ConflictException("Option not found");

                if (option.QuestionId != question.Id)
                    throw new ConflictException("Invalid option for question");
            }

            var existing = _dbContext.UserAnswers
                .FirstOrDefault(a =>
                    a.AttemptId == request.AttemptId &&
                    a.QuestionId == question.Id);

            if (existing != null)
            {
                existing.SelectedOptionId = selectedOptionId;
            }
            else
            {
                _dbContext.UserAnswers.Add(new UserAnswer
                {
                    AttemptId = request.AttemptId,
                    QuestionId = question.Id,
                    SelectedOptionId = selectedOptionId
                });
            }
        }

        _dbContext.SaveChanges();

        return CalculateResult(attempt.Id);
    }

    private UserAttemptDto CalculateResult(int attemptId)
    {
        var attempt = _dbContext.UserAttempts
            .FirstOrDefault(a => a.Id == attemptId);

        if (attempt == null)
            throw new ConflictException("Attempt not found");

        var answers = _dbContext.UserAnswers
            .Where(a => a.AttemptId == attemptId)
            .ToList();

        var score = (
            from ans in answers
            join opt in _dbContext.Options
            on ans.SelectedOptionId equals opt.Id
            where opt.IsCorrect
            select ans
        ).Count();

        attempt.TotalScore = score;
        attempt.IsPassed = score > 0;

        _dbContext.SaveChanges();

        return new UserAttemptDto(
            attempt.Id,
            attempt.UserId,
            attempt.TestId,
            attempt.TotalScore,
            attempt.IsPassed,
            attempt.AttemptDate
        );
    }
}
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

        var test = _dbContext.Tests.Find(request.TestId);
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
            AttemptDate = DateTime.Now
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

    public void SubmitAnswer(CreateSubmitAnswerRequest request)
    {
        if (request.AttemptId <= 0 || request.QuestionId <= 0 || request.SelectedOptionId <= 0)
            throw new ConflictException("Invalid data");

        var attempt = _dbContext.UserAttempts.FirstOrDefault(a => a.Id == request.AttemptId);
        if (attempt == null)
            throw new ConflictException("Attempt not found");

        var question = _dbContext.Questions.FirstOrDefault(q => q.Id == request.QuestionId);
        if (question == null)
            throw new ConflictException("Question not found");

        if (question.TestId != attempt.TestId)
            throw new ConflictException("Question does not belong to this test");

        var option = _dbContext.Options.FirstOrDefault(o => o.Id == request.SelectedOptionId);
        if (option == null)
            throw new ConflictException("Option not found");

        if (option.QuestionId != request.QuestionId)
            throw new ConflictException("Option does not belong to this question");

        var alreadyAnswered = _dbContext.UserAnswers
            .Any(a => a.AttemptId == request.AttemptId && a.QuestionId == request.QuestionId);

        if (alreadyAnswered)
            throw new ConflictException("Question already answered");

        var answer = new UserAnswer
        {
            AttemptId = request.AttemptId,
            QuestionId = request.QuestionId,
            SelectedOptionId = request.SelectedOptionId
        };

        _dbContext.UserAnswers.Add(answer);
        _dbContext.SaveChanges();
    }

    public UserAttemptDto SubmitTest(int attemptId)
    {
        if (attemptId <= 0)
            throw new ConflictException("Invalid AttemptId");

        var attempt = _dbContext.UserAttempts.FirstOrDefault(a => a.Id == attemptId);
        if (attempt == null)
            throw new ConflictException("Attempt not found");

        var answers = _dbContext.UserAnswers
            .Where(a => a.AttemptId == attemptId)
            .ToList();

        var correctOptionIds = _dbContext.Options
            .Where(o => o.IsCorrect)
            .Select(o => o.Id)
            .ToHashSet();

        var score = answers.Count(a => correctOptionIds.Contains(a.SelectedOptionId));

        attempt.TotalScore = score;
        attempt.IsPassed = score >= 1;

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
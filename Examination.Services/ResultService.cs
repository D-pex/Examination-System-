using Examination.Core.Dtos;
using Examination.Persistence;
using Examination.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

public sealed class ResultService
{
    private readonly AppDbContext _dbContext;

    public ResultService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<ResultDto> GetAllResults()
    {
        var results = _dbContext.UserAttempts
            .AsNoTracking()
            .Include(ua => ua.User)
            .Include(ua => ua.Test)
            .Select(ua => new ResultDto(
                ua.User != null ? ua.User.Name : "",
                ua.Test != null ? ua.Test.Name : "",
                ua.TotalScore,
                ua.IsPassed,
                ua.AttemptDate
            ))
            .ToList();

        return results;
    }

    public List<ResultDto> GetResultsByUser(int userId)
    {
        if (userId <= 0)
            throw new ConflictException("Invalid UserId");

        var results = _dbContext.UserAttempts
            .AsNoTracking()
            .Where(ua => ua.UserId == userId)
            .Include(ua => ua.User)
            .Include(ua => ua.Test)
            .Select(ua => new ResultDto(
                ua.User != null ? ua.User.Name : "",
                ua.Test != null ? ua.Test.Name : "",
                ua.TotalScore,
                ua.IsPassed,
                ua.AttemptDate
            ))
            .ToList();

        return results;
    }

    public ResultDto GetResultByAttempt(int attemptId)
    {
        if (attemptId <= 0)
            throw new ConflictException("Invalid AttemptId");

        var result = _dbContext.UserAttempts
            .AsNoTracking()
            .Include(ua => ua.User)
            .Include(ua => ua.Test)
            .Where(ua => ua.Id == attemptId)
            .Select(ua => new ResultDto(
                ua.User != null ? ua.User.Name : "",
                ua.Test != null ? ua.Test.Name : "",
                ua.TotalScore,
                ua.IsPassed,
                ua.AttemptDate
            ))
            .FirstOrDefault();

        if (result == null)
            throw new ConflictException("Result not found");

        return result;
    }
}
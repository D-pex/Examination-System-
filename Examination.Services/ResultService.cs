using Examination.Core.Dtos;
using Examination.Persistence;
using Examination.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Examination.Services;

public sealed class ResultService
{
    private readonly AppDbContext _dbContext;

    public ResultService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<ResultDto> GetAllResults()
    {
        return _dbContext.UserAttempts
            .Include(ua => ua.User)
            .Include(ua => ua.Test)
            .Select(ua => new ResultDto
            {
                AttemptId = ua.Id,
                UserId = ua.UserId,
                UserName = ua.User != null ? ua.User.Name : "",
                TestId = ua.TestId,
                TestName = ua.Test != null ? ua.Test.Name : "",
                TotalScore = ua.TotalScore,
                IsPassed = ua.IsPassed,
                AttemptDate = ua.AttemptDate
            })
            .ToList();
    }
    
   

    public List<ResultDto> GetResultsByUser(int userId)
    {
       
        if (userId <= 0)
            throw new ConflictException("Invalid UserId");

        return _dbContext.UserAttempts
            .Where(ua => ua.UserId == userId)
            .Include(ua => ua.User)
            .Include(ua => ua.Test)
            .Select(ua => new ResultDto
            {
                AttemptId = ua.Id,
                UserId = ua.UserId,
                UserName = ua.User != null ? ua.User.Name : "",
                TestId = ua.TestId,
                TestName = ua.Test != null ? ua.Test.Name : "",
                TotalScore = ua.TotalScore,
                IsPassed = ua.IsPassed,
                AttemptDate = ua.AttemptDate
            })
            .ToList();
    }
    

    public ResultDto GetResultByAttempt(int attemptId)
    {
        if (attemptId <= 0)
            throw new ConflictException("Invalid AttemptId");

        var result = _dbContext.UserAttempts
            .Include(ua => ua.User)
            .Include(ua => ua.Test)
            .Where(ua => ua.Id == attemptId)
            .Select(ua => new ResultDto
            {
                AttemptId = ua.Id,
                UserId = ua.UserId,
                UserName = ua.User != null ? ua.User.Name : "",
                TestId = ua.TestId,
                TestName = ua.Test != null ? ua.Test.Name : "",
                TotalScore = ua.TotalScore,
                IsPassed = ua.IsPassed,
                AttemptDate = ua.AttemptDate
            })
            .FirstOrDefault();

        if (result == null)
            throw new ConflictException("Result not found");

        return result;
    }

   
}

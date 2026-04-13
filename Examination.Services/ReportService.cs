using Examination.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Examination.Services;

public sealed class ReportService
{
    private readonly AppDbContext _dbContext;

    public ReportService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<ReportDto> GetTestReports()
    {
        return _dbContext.UserAttempts
            .Include(ua => ua.Test)
            .GroupBy(ua => new { ua.TestId, ua.Test!.Name })
            .Select(ua => new ReportDto
            {
                TestId = ua.Key.TestId,
                TestName = ua.Key.Name,
                TotalAttempts = ua.Count(),
                PassedCount = ua.Count(x => x.IsPassed),
                FailedCount = ua.Count(x => !x.IsPassed),
                AverageScore = ua.Average(x => x.TotalScore)
            })
            .ToList();
    }
}
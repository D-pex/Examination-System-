using Examination.Core.Dtos;
using Examination.Persistence;
using Microsoft.EntityFrameworkCore;

public sealed class ReportService
{
    private readonly AppDbContext _dbContext;

    public ReportService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<ReportDto> GetTestReports()
    {
        var reports = _dbContext.Tests
            .AsNoTracking()
            .GroupJoin(
                _dbContext.UserAttempts,
                t => t.Id,
                ua => ua.TestId,
                (t, attempts) => new ReportDto(
                    t.Name,
                    attempts.Count()
                )
            )
            .ToList();

        return reports;
    }
}
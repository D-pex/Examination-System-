using Examination.Core.Dtos;
using Examination.Core.Requests;
using Examination.Persistence;
using Examination.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

public sealed class TestService
{
    private readonly AppDbContext _dbContext;

    public TestService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public TestDto CreateTest(CreateTestRequest request)
    {
        if (string.IsNullOrEmpty(request.Name))
            throw new ConflictException("Test name is required");

        var existing = _dbContext.Tests
            .AsNoTracking()
            .FirstOrDefault(t => t.Name == request.Name.Trim());

        if (existing != null)
            throw new ConflictException("Test already exists");


        var test = new Test
        {
            Name = request.Name.Trim(),
            Subject = request.Subject?.Trim() ?? "",
            Description = request.Description?.Trim() ?? "",
            Duration = request.Duration,
            IsPublished = false,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Tests.Add(test);
        _dbContext.SaveChanges();


        return new TestDto(
            test.Id,
            test.Name,
            test.Subject,
            test.Description,
            test.Duration,
            test.IsPublished,
            test.CreatedAt
        );
    }

    public IEnumerable<TestDto> GetAllTests()
    {
        return _dbContext.Tests
            .Select(t => new TestDto(
                t.Id,
                t.Name,
                t.Subject,
                t.Description,
                t.Duration,
                t.IsPublished,
                t.CreatedAt
            ))
            .ToList();
    }

    public List<TestDto> GetPublishedTests()
    {
        return _dbContext.Tests
            .Where(t => t.IsPublished)
            .Select(t => new TestDto(
                t.Id,
                t.Name ?? "",
                t.Subject ?? "",
                t.Description ?? "",
                t.Duration,
                t.IsPublished,
                t.CreatedAt
            ))
            .ToList();
    }

    public TestDto GetTestById(int id)
    {
        if (id <= 0)
            throw new ConflictException("Invalid test ID");

        var test = _dbContext.Tests
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

        if (test == null)
            throw new NotFoundException($"Test with ID {id} not found");


        return new TestDto(
            test.Id,
            test.Name ?? "",
            test.Subject ?? "",
            test.Description ?? "",
            test.Duration,
            test.IsPublished,
            test.CreatedAt
        );
    }

    public void PublishTest(int id)
    {
        if (id <= 0)
            throw new ConflictException("Invalid test ID");

        var test = _dbContext.Tests.FirstOrDefault(x => x.Id == id);

        if (test == null)
            throw new NotFoundException($"Test with ID {id} not found");

        if (test.IsPublished)
            throw new ConflictException("Test is already published");

        test.IsPublished = true;
        _dbContext.SaveChanges();
    }
}
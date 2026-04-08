using Examination.Core.Dtos;
using Examination.Core.Requests;
using Examination.Persistence;
using Examination.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

public sealed class TestService
{
    private readonly AppDbContext _dbContext;

    public TestService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public TestDto CreateTest(CreateTestRequest request)
    {
        try
        {
            if (request.Name == null || request.Name == "")
                throw new ConflictException("Test name is required");

            var existing = _dbContext.Tests.FirstOrDefault(t => t.Name == request.Name);

            if (existing != null)
                throw new ConflictException("Test already exists");

            var test = new Test
            {
                Name = request.Name,
                Subject = request.Subject,
                Description = request.Description,
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
                test.Duration
            );
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }

    public List<TestDto> GetPublishedTests()
    {
        try
        {
            return _dbContext.Tests
                .Where(t => t.IsPublished)
                .Select(t => new TestDto(
                    t.Id,
                    t.Name,
                    t.Subject,
                    t.Duration
                ))
                .ToList();
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }

    public void PublishTest(int id)
    {
        try
        {
            var test = _dbContext.Tests.FirstOrDefault(x => x.Id == id);

            if (test == null)
                throw new ConflictException("Test not found");

            test.IsPublished = true;

            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }
    }
}
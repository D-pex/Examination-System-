namespace Examination.Core.Dtos;

public sealed class TestDto(
    int Id,
    string Name,
    string Subject,
    string? Description,
    int Duration,
    bool IsPublished,
    DateTime CreatedAt
);
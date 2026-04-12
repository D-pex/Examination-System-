namespace Examination.Persistence;

public sealed class Test
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int Duration { get; set; }

    public bool IsPublished { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<Question> Questions { get; set; } = new();

    public List<UserAttempt> UserAttempts { get; set; } = new();
}
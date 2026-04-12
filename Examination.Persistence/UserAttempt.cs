namespace Examination.Persistence;

public sealed  class UserAttempt
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TestId { get; set; }

    public int TotalScore { get; set; }

    public bool IsPassed { get; set; }

    public DateTime AttemptDate { get; set; }

    public User? User { get; set; }

    public Test? Test { get; set; }

    public List<UserAnswer> UserAnswers { get; set; } = new();
}
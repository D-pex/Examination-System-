namespace Examination.Core.Dtos;

public sealed class UserAttemptDto(
    int Id,
    int UserId,
    int TestId,
    int TotalScore,
    bool IsPassed,
    DateTime AttemptDate
)
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TestId { get; set; }
    public int TotalScore { get; set; }
    public bool IsPassed { get; set; }
    public DateTime AttemptDate { get; set; }
}
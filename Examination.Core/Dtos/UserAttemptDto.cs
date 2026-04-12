namespace Examination.Core.Dtos;

public sealed class UserAttemptDto(
    int Id,
    int UserId,
    int TestId,
    int TotalScore,
    bool IsPassed,
    DateTime AttemptDate
);
namespace Examination.Core.Dtos;
public sealed class ResultDto(
    string UserName,
    string TestName,
    int TotalScore,
    bool IsPassed,
    DateTime AttemptDate
);
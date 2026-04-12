namespace Examination.Core.Dtos;

public sealed class UserAnswerDto(
    int Id,
    int AttemptId,
    int QuestionId,
    int SelectedOptionId
);
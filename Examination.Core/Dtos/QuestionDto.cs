namespace Examination.Core.Dtos;

public sealed class QuestionDto(
    int Id,
    string QuestionText,
    List<OptionDto> Options
);
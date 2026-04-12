namespace Examination.Core.Requests;

public sealed class CreateQuestionRequest
{
    public int TestId { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public List<CreateOptionRequest> Options { get; set; } = new();
}
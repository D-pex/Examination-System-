namespace Examination.Core.Requests;

public sealed class CreateSubmitAnswerRequest
{
    public int AttemptId { get; set; }
    public int QuestionId { get; set; }
    public int SelectedOptionId { get; set; }
}
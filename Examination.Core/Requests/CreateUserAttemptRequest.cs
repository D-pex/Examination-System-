namespace Examination.Core.Requests;

public sealed class CreateUserAttemptRequest
{
    public int QuestionId { get; set; }
    public int SelectedOptionId { get; set; }
    public int UserId { get; set; }
    public int TestId { get; set; }
}
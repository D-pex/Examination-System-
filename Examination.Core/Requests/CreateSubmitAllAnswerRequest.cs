public sealed class CreateSubmitAllAnswerRequest
{
    public int AttemptId { get; set; }
    public List<AnswerItem> Answers { get; set; } = new();
}

public sealed class AnswerItem
{
    public int QuestionId { get; set; }
    public int SelectedOptionId { get; set; }
}
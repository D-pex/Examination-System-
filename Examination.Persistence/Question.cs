namespace Examination.Persistence;

public sealed class Question
{
    public int Id { get; set; }

    public int TestId { get; set; }

    public string QuestionText { get; set; } = string.Empty;

    public Test? Test { get; set; }

    public List<Option> Options { get; set; } = new();

    public List<UserAnswer> UserAnswers { get; set; } = new();
}
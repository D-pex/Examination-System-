namespace Examination.Persistence;

public sealed class UserAnswer
{
    public int Id { get; set; }

    public int AttemptId { get; set; }

    public int QuestionId { get; set; }

    public int SelectedOptionId { get; set; }

    public UserAttempt? UserAttempt { get; set; }

    public Question? Question { get; set; }

    public Option? SelectedOption { get; set; }
}
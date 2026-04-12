namespace Examination.Core.Requests;

public sealed class CreateOptionRequest
{
    public string OptionText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}
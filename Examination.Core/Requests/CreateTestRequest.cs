namespace Examination.Core.Requests;

public sealed class CreateTestRequest
{
    public string Name { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Duration { get; set; }
}
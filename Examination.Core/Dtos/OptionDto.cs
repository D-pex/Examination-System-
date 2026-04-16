public sealed class OptionDto
{
    public int Id { get; set; }
    public string OptionText { get; set; }

    public OptionDto(int id, string optionText)
    {
        Id = id;
        OptionText = optionText;
    }
}
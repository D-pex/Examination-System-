using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Persistence;
public class Question
{
    public int Id { get; set; }
    public int TestId { get; set; }
    public string QuestionText { get; set; }
    public Test Test { get; set; }
    public List<Option> Options { get; set; } = new();
    public List<UserAnswer> UserAnswers { get; set; } = new();
}

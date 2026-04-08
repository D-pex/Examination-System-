using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Persistence;
public class Option
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string OptionText { get; set; }
    public bool IsCorrect { get; set; }

    public Question Question { get; set; }
}
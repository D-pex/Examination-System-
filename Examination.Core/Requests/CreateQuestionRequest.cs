using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Core.Requests;

public sealed record CreateQuestionRequest 
{     public int TestId { get; set; }
    public string QuestionText { get; set; }
    public List<CreateOptionRequest> Options { get; set; } = new();
}